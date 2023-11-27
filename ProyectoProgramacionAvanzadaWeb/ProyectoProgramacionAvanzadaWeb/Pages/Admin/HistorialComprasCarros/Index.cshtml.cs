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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.HistorialComprasCarros
{
    public class IndexModel : PageModel
    {
        private readonly HistorialCompraCarroApiService _historialCompraCarroApiService;

        public string Message { get; set; }

        public IndexModel(HistorialCompraCarroApiService historialCompraCarroApiService)
        {
            _historialCompraCarroApiService = historialCompraCarroApiService;
        }

        public IList<HistorialCompraCarro> HistorialCompraCarro { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var (historialCompraCarro, errorMessage) = await _historialCompraCarroApiService.ObtenerHistorialesAsync();

            if (historialCompraCarro != null)
            {
                HistorialCompraCarro = historialCompraCarro;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener historial de compras de carros desde la API.";
            }
        }
    }
}
