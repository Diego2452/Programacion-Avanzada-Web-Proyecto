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
    public class DeleteModel : PageModel
    {
        private readonly TipoFinanciamientosApiService _tipoFinanciamientosApiService;

        public string Message { get; set; }

        public DeleteModel(TipoFinanciamientosApiService tipoFinanciamientosApiService)
        {
            _tipoFinanciamientosApiService = tipoFinanciamientosApiService;
        }

        [BindProperty]
      public TipoFinanciamientos TipoFinanciamientos { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (tipoFinanciamientos, message) = await _tipoFinanciamientosApiService.ObtenerDetallesTipoFinanciamientoAsync(id.Value);

            if (tipoFinanciamientos != null)
            {
                TipoFinanciamientos = tipoFinanciamientos;
            }
            else
            {
                Message = message ?? "Error al obtener el tipo de financiamiento desde la API.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (success, message) = await _tipoFinanciamientosApiService.EliminarTipoFinanciamientoAsync(id.Value);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = message ?? "Error al obtener el tipo de financiamiento desde la API.";
                return Page();
            }
        }
    }
}
