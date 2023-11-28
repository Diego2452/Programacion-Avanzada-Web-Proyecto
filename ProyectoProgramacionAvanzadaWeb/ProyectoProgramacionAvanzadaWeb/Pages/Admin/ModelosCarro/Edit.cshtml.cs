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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.ModelosCarro
{
    public class EditModel : PageModel
    {
        private readonly MarcasCarrosApiService _marcasCarrosApiService;
        private readonly ModelosCarrosApiService _modelosCarrosApiService;

        public string Message { get; set; }

        public EditModel(
            ModelosCarrosApiService modelosCarrosApiService,
            MarcasCarrosApiService marcasCarrosApiService)
        {
            _modelosCarrosApiService = modelosCarrosApiService;
            _marcasCarrosApiService = marcasCarrosApiService;
        }

        [BindProperty]
        public ModelosCarros ModelosCarros { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (modeloCarro, message) = await _modelosCarrosApiService.ObtenerDetallesModeloCarroAsync(id.Value);

            if (modeloCarro != null)
            {
                ModelosCarros = modeloCarro;
                await SelectList();
            }
            else
            {
                Message = message ?? "Error al obtener el modelo de carro desde la API.";
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

            var (success, message) = await _modelosCarrosApiService.EditarModeloCarroAsync(ModelosCarros);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Edit", new { id = ModelosCarros.IdModelo });
            }
            else
            {
                Message = message ?? "Error al actualizar el modelo de carro.";
                await SelectList();
                return Page();
            }
        }

        private async Task SelectList()
        {
            var (marcaCarro, _) = await _marcasCarrosApiService.ObtenerMarcasCarrosAsync();
            ViewData["IdMarca"] = marcaCarro != null ? new SelectList(marcaCarro, "IdMarca", "Marca") : null;
        }
    }
}
