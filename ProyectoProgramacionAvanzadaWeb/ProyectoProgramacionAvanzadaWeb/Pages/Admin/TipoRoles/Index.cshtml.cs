using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;
using System.Net;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoRoles
{
    public class IndexModel : PageModel
    {
        private readonly RolesApiService _rolesApiService;

        public string Message { get; set; }

        public IndexModel(RolesApiService rolesApiService)
        {
            _rolesApiService = rolesApiService;
        }

        public IList<Roles> Roles { get; set; } = new List<Roles>();

        public async Task OnGetAsync()
        {
            var (roles, errorMessage) = await _rolesApiService.ObtenerRolesAsync();

            if (roles != null)
            {
                Roles = roles;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener roles desde la API.";
            }
        }
    }
}