using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Usuario
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly UsuarioApiService _usuarioApiService;
        public string Message { get; set; }

        public EditModel(IConfiguration configuration, UsuarioApiService usuarioApiService)
        {
            _configuration = configuration;
            _usuarioApiService = usuarioApiService;
        }

        [BindProperty]
        public Usuarios Usuarios { get; set; } = new Usuarios();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (usuario, message) = await _usuarioApiService.ObtenerDetallesUsuarioAsync(id.Value);

            if (usuario != null)
            {
                Usuarios = usuario;
                await SelectLists();
            }
            else
            {
                Message = message ?? "Error al obtener el usuario desde la API.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SelectLists();
                return Page();
            }

            var (success, message) = await _usuarioApiService.EditarUsuarioAsync(Usuarios);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Edit", new { id = Usuarios.IdUsuario });
            }
            else
            {
                Message = message ?? "Error al actualizar el usuario.";
                await SelectLists();
                return Page();
            }
        }

        private async Task SelectLists()
        {
            ViewData["IdTipoIdentificacion"] = new SelectList(await GetTipoIdentificacionesAsync(), "IdIdentificacion", "TipoIdentificacion");
            ViewData["IdGenero"] = new SelectList(await GetGenerosAsync(), "IdGenero", "TipoGenero");
            ViewData["IdRol"] = new SelectList(await GetRolesAsync(), "IdRol", "NombreRol");
        }
        public async Task<List<TipoIdentificaciones>> GetTipoIdentificacionesAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "tipoidentificaciones";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<TipoIdentificaciones> tipoIdentificaciones = JsonConvert.DeserializeObject<List<TipoIdentificaciones>>(content);
                    return tipoIdentificaciones;
                }
                return null;
            }
        }
        public async Task<List<Genero>> GetGenerosAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "Generos";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<Genero> Generos = JsonConvert.DeserializeObject<List<Genero>>(content);
                    return Generos;
                }
                return null;
            }
        }
        public async Task<List<Roles>> GetRolesAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "roles";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<Roles> roles = JsonConvert.DeserializeObject<List<Roles>>(content);
                    return roles;
                }
                return null;
            }
        }
    }
}
