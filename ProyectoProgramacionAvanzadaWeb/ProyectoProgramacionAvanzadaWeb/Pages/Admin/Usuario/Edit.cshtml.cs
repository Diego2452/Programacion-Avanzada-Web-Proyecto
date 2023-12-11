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
        private readonly UsuarioApiService _usuarioApiService;
        TipoIdentificacionesApiService _tipoIdentificacionesApiService;
        RolesApiService _rolapiService;
        GenerosApiService _generoApiService;
        public string Message { get; set; }

        public EditModel(
            UsuarioApiService usuarioApiService,
            TipoIdentificacionesApiService tipoIdentificacionesApiService,
            RolesApiService rolapiService,
            GenerosApiService generoApiService
            )
        {
            _usuarioApiService = usuarioApiService;
            _tipoIdentificacionesApiService = tipoIdentificacionesApiService;
            _rolapiService = rolapiService;
            _generoApiService = generoApiService;

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
            var (tipoIdentificacion, _) = await _tipoIdentificacionesApiService.ObtenerTipoIdentificacionesAsync();
            ViewData["IdTipoIdentificacion"] = tipoIdentificacion != null ? new SelectList(tipoIdentificacion, "IdIdentificacion", "TipoIdentificacion") : null;

            var (genero, _) = await _generoApiService.ObtenerGenerosAsync();
            ViewData["IdGenero"] = genero != null ? new SelectList(genero, "IdGenero", "TipoGenero") : null;

            var(rol, _) = await _rolapiService.ObtenerRolesAsync();
            ViewData["IdRol"] = rol != null ? new SelectList(rol, "IdRol", "NombreRol") : null;
        }

    }
}
