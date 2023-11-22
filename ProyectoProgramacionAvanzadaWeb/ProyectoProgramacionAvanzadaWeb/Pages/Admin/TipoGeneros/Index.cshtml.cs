using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;
using System.Net;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoGeneros
{
    public class IndexModel : PageModel
    {
        private readonly GenerosApiService _generosApiService;

        public string Message { get; set; }

        public IndexModel(GenerosApiService generosApiService)
        {
            _generosApiService = generosApiService;
        }
        public IList<Genero> Generos { get; set; } = new List<Genero>();

        public async Task OnGetAsync()
        {
            var (generos, errorMessage) = await _generosApiService.ObtenerGenerosAsync();

            if (generos != null)
            {
                Generos = generos;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener generos desde la API.";
            }
        }
    }
}
