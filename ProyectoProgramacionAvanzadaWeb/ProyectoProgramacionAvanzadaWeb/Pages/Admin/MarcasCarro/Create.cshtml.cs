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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.MarcasCarro
{
    public class CreateModel : PageModel
    {
        private readonly MarcasCarrosApiService _marcasCarrosApiService;

        public string Message { get; set; }

        public CreateModel(MarcasCarrosApiService marcasCarrosApiService)
        {
            _marcasCarrosApiService = marcasCarrosApiService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MarcasCarros MarcasCarros { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (success, message) = await _marcasCarrosApiService.CrearMarcaCarroAsync(MarcasCarros);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Create");
            }
            else
            {
                Message = message ?? "Error al crear la marca de carro";
                return Page();
            }
        }
    }
}
