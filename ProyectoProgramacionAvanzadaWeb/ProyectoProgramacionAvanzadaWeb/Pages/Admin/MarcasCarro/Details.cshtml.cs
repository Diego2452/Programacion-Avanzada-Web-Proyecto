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
    public class DetailsModel : PageModel
    {
        private readonly MarcasCarrosApiService _marcasCarrosApiService;

        public string Message { get; set; }

        public DetailsModel(MarcasCarrosApiService marcasCarrosApiService)
        {
            _marcasCarrosApiService = marcasCarrosApiService;
        }

        public MarcasCarros MarcasCarros { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (marcaCarro, message) = await _marcasCarrosApiService.ObtenerDetallesMarcaCarroAsync(id.Value);

            if (marcaCarro != null)
            {
                MarcasCarros = marcaCarro;
            }
            else
            {
                Message = message ?? "Error al obtener la marca de carro desde la API.";
            }

            return Page();
        }
    }
}
