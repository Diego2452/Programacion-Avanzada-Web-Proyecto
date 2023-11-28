using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoFinanciamiento
{
    public class CreateModel : PageModel
    {
        private readonly TipoFinanciamientosApiService _tipoFinanciamientosApiService;

        public string Message { get; set; }

        public CreateModel(TipoFinanciamientosApiService tipoFinanciamientosApiService)
        {
            _tipoFinanciamientosApiService = tipoFinanciamientosApiService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TipoFinanciamientos TipoFinanciamientos { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (success, message) = await _tipoFinanciamientosApiService.CrearTipoFinanciamientoAsync(TipoFinanciamientos);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Create");
            }
            else
            {
                Message = message ?? "Error al crear el tipo de financiamiento.";
                return Page();
            }
        }
    }
}
