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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoCombustible
{
    public class DetailsModel : PageModel
    {
        private readonly TipoCombustiblesApiService _tipoCombustibleApiService;

        public string Message { get; set; }

        public DetailsModel(TipoCombustiblesApiService tipoCombustibleApiService)
        {
            _tipoCombustibleApiService = tipoCombustibleApiService;
        }

        public TipoCombustibles TipoCombustibles { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (tipoCombustible, message) = await _tipoCombustibleApiService.ObtenerDetallesTipoCombustibleAsync(id.Value);

            if (tipoCombustible != null)
            {
                TipoCombustibles = tipoCombustible;
            }
            else
            {
                Message = message ?? "Error al obtener el tipo combustible desde la API.";
            }

            return Page();
        }
    }
}
