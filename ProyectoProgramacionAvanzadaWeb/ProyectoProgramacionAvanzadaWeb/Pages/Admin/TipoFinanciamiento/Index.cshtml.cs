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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoFinanciamiento
{
    public class IndexModel : PageModel
    {
        private readonly TipoFinanciamientosApiService _tipoFinanciamientosApiService;

        public string Message { get; set; }

        public IndexModel(TipoFinanciamientosApiService tipoFinanciamientosApiService)
        {
            _tipoFinanciamientosApiService = tipoFinanciamientosApiService;
        }

        public IList<TipoFinanciamientos> TipoFinanciamientos { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var (tipoFinanciamientos, errorMessage) = await _tipoFinanciamientosApiService.ObtenerTipoFinanciamientosAsync();

            if (tipoFinanciamientos != null)
            {
                TipoFinanciamientos = tipoFinanciamientos;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener los tipos de financiamientos desde la API.";
            }
        }
    }
}
