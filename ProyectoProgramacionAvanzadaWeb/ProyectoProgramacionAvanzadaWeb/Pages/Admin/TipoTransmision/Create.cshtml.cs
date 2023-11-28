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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoTransmision
{
    public class CreateModel : PageModel
    {
        private readonly ProyectoProgramacionAvanzadaWeb.Data.ProyectoProgramacionAvanzadaWebContext _context;
        private readonly TipoTransmisionesApiService _tipoTransmisionesApiService;

        public string Message { get; set; }

        public CreateModel(TipoTransmisionesApiService tipoTransmisionesApiService)
        {
            _tipoTransmisionesApiService = tipoTransmisionesApiService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TipoTransmisiones TipoTransmisiones { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (success, message) = await _tipoTransmisionesApiService.CrearTipoTransmisionAsync(TipoTransmisiones);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Create");
            }
            else
            {
                Message = message ?? "Error al crear el tipo de transmision.";
                return Page();
            }
        }
    }
}
