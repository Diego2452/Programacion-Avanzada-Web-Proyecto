using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;
using System.Net;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Usuario
{
    public class IndexModel : PageModel
    {

        private readonly UsuarioApiService _usuarioApiService;

        public string Message { get; set; }

        public IndexModel(UsuarioApiService usuarioApiService)
        {
            _usuarioApiService = usuarioApiService;
        }

        public IList<Usuarios> Usuarios { get; set; } = new List<Usuarios>();

        public async Task OnGetAsync()
        {
            var (usuarios, errorMessage) = await _usuarioApiService.ObtenerUsuariosAsync();

            if (usuarios != null)
            {
                Usuarios = usuarios;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener usuarios desde la API.";
            }
        }
    }
}