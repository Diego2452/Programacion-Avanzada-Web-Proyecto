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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Facturas
{
    public class IndexModel : PageModel
    {
        private readonly FacturacionApiService _facturacionApiService;

        public string Message { get; set; }

        public IndexModel(FacturacionApiService facturacionApiService)
        {
            _facturacionApiService = facturacionApiService;
        }

        public IList<Facturacion> Facturacion { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var (facturas, errorMessage) = await _facturacionApiService.ObtenerFacturasAsync();

            if (facturas != null)
            {
                Facturacion = facturas;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener las facturas desde la API.";
            }
        }
    }
}
