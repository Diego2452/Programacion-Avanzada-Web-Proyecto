using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Usuario
{
    public class CreateModel : PageModel
    {
        private readonly UsuarioApiService _usuarioApiService;
        private readonly TipoIdentificacionesApiService _tipoIdentificacionesApiService;
        private readonly GenerosApiService _generosApiService;
        private readonly RolesApiService _rolesApiService;
        public string Message { get; set; }

        public CreateModel( 
            UsuarioApiService usuarioApiService,
            TipoIdentificacionesApiService tipoIdentificacionesApiService,
            GenerosApiService generosApiService,
            RolesApiService rolesApiService)
        {
            _usuarioApiService = usuarioApiService;
            _tipoIdentificacionesApiService = tipoIdentificacionesApiService;
            _generosApiService = generosApiService;
            _rolesApiService = rolesApiService;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                await SelectLists();
                return Page();
            }
            catch (Exception ex)
            {
                Message = "Error interno del servidor: " + ex.Message;
                return Page();
            }
        }

        [BindProperty]
        public Usuarios Usuarios { get; set; } = new Usuarios();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SelectLists();
                return Page();
            }

            var (success, message) = await _usuarioApiService.CrearUsuarioAsync(Usuarios);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Create");
            }
            else
            {
                Message = message ?? "Error al crear el usuario.";
                await SelectLists();
                return Page();
            }
        }

        private async Task SelectLists()
        {           
            var (tipoIdentificaciones, _) = await _tipoIdentificacionesApiService.ObtenerTipoIdentificacionesAsync();
            ViewData["IdTipoIdentificacion"] = tipoIdentificaciones != null ? new SelectList(tipoIdentificaciones, "IdIdentificacion", "TipoIdentificacion") : null;

            var (roles, _) = await _rolesApiService.ObtenerRolesAsync();
            ViewData["IdRol"] = roles != null ? new SelectList(roles, "IdRol", "NombreRol") : null;

            var (generos, _) = await _generosApiService.ObtenerGenerosAsync();
            ViewData["IdGenero"] = generos != null ? new SelectList(generos, "IdGenero", "TipoGenero") : null;
            Console.WriteLine(generos);
        }

    }
}
