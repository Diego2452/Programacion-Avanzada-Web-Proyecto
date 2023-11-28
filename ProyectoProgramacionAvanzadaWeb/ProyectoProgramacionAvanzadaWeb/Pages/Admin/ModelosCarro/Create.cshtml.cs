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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.ModelosCarro
{
    public class CreateModel : PageModel
    {
        private readonly MarcasCarrosApiService _marcasCarrosApiService;
        private readonly ModelosCarrosApiService _modelosCarrosApiService;

        public string Message { get; set; }

        public CreateModel(
            ModelosCarrosApiService modelosCarrosApiService,
            MarcasCarrosApiService marcasCarrosApiService)
        {
            _modelosCarrosApiService = modelosCarrosApiService;
            _marcasCarrosApiService = marcasCarrosApiService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await SelectList();
                return Page();
            }
            catch (Exception ex)
            {
                Message = "Error interno del servidor: " + ex.Message;
                return Page();
            }
        }

        [BindProperty]
        public ModelosCarros ModelosCarros { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (success, message) = await _modelosCarrosApiService.CrearModeloCarroAsync(ModelosCarros);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Create");
            }
            else
            {
                Message = message ?? "Error al crear el modelo de carro";
                await SelectList();
                return Page();
            };
        }

        private async Task SelectList()
        {
            var (marcaCarro, _) = await _marcasCarrosApiService.ObtenerMarcasCarrosAsync();
            ViewData["IdMarca"] = marcaCarro != null ? new SelectList(marcaCarro, "IdMarca", "Marca") : null;
        }
    }
}
