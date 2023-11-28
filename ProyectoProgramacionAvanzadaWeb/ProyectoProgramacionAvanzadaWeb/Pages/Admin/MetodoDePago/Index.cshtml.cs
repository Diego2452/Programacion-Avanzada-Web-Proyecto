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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.MetodoDePago
{
    public class IndexModel : PageModel
    {
        private readonly MetodosDePagoApiService _metodosDePagoApiService;

        public string Message { get; set; }

        public IndexModel(MetodosDePagoApiService metodosDePagoApiService)
        {
            _metodosDePagoApiService = metodosDePagoApiService;
        }

        public IList<MetodosDePago> MetodosDePago { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var (metodosDePago, errorMessage) = await _metodosDePagoApiService.ObtenerMetodosDePagoAsync();

            if (metodosDePago != null)
            {
                MetodosDePago = metodosDePago;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener los metodos de pago desde la API.";
            }
        }
    }
}
