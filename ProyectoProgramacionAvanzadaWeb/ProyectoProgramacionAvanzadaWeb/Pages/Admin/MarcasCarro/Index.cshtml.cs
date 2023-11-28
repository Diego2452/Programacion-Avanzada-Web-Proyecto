using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.MarcasCarro
{
    public class IndexModel : PageModel
    {
        private readonly MarcasCarrosApiService _marcasCarrosApiService;

        public string Message { get; set; }

        public IndexModel(MarcasCarrosApiService marcasCarrosApiService)
        {
            _marcasCarrosApiService = marcasCarrosApiService;
        }

        public IList<MarcasCarros> MarcasCarros { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var (marcasCarros, errorMessage) = await _marcasCarrosApiService.ObtenerMarcasCarrosAsync();

            if (marcasCarros != null)
            {
                MarcasCarros = marcasCarros;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener las marcas de carros desde la API.";
            }
        }
    }
}
