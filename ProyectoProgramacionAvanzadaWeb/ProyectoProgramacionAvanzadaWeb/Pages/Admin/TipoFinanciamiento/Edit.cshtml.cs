using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoFinanciamiento
{
    public class EditModel : PageModel
    {
        private readonly TipoFinanciamientosApiService _tipoFinanciamientosApiService;

        public string Message { get; set; }

        public EditModel(TipoFinanciamientosApiService tipoFinanciamientosApiService)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (success, message) = await _tipoFinanciamientosApiService.EditarTipoFinanciamientoAsync(TipoFinanciamientos);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Edit", new { id = TipoFinanciamientos.IdFinanciamiento });
            }
            else
            {
                Message = message ?? "Error al actualizar el tipo de financiamiento.";
                return Page();
            }
        }
    }
}
