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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.MetodoDePago
{
    public class CreateModel : PageModel
    {
        private readonly MetodosDePagoApiService _metodosDePagoApiService;

        public string Message { get; set; }

        public CreateModel(MetodosDePagoApiService metodosDePagoApiService)
        {
            _metodosDePagoApiService = metodosDePagoApiService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MetodosDePago MetodosDePago { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (success, message) = await _metodosDePagoApiService.CrearMetodoDePagoAsync(MetodosDePago);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Create");
            }
            else
            {
                Message = message ?? "Error al crear el metodo de pago.";
                return Page();
            }
        }
    }
}
