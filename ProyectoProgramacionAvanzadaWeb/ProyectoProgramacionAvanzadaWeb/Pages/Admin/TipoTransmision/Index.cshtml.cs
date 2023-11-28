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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoTransmision
{
    public class IndexModel : PageModel
    {
        private readonly TipoTransmisionesApiService _tipoTransmisionesApiService;

        public string Message { get; set; }

        public IndexModel(TipoTransmisionesApiService tipoTransmisionesApiService)
        {
            _tipoTransmisionesApiService = tipoTransmisionesApiService;
        }

        public IList<TipoTransmisiones> TipoTransmisiones { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var (tipoTransmisiones, errorMessage) = await _tipoTransmisionesApiService.ObtenerTipoTransmisionesAsync();

            if (tipoTransmisiones != null)
            {
                TipoTransmisiones = tipoTransmisiones;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener los tipos de transmisiones desde la API.";
            }
        }
    }
}
