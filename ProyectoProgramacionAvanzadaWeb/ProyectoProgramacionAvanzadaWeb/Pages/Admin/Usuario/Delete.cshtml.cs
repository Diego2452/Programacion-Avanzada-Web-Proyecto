using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Usuario
{
    public class DeleteModel : PageModel
    {
        private readonly UsuarioApiService _usuarioApiService;
        public string Message { get; set; }
        public DeleteModel(UsuarioApiService usuarioApiService)
        {
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
            }
            else
            {
                Message = message ?? "Error al obtener el usuario desde la API.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (success, message) = await _usuarioApiService.EliminarUsuarioAsync(id.Value);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = message ?? "Error al obtener el usuario desde la API.";
                return Page();
            }
        }
    }
}