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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.ModelosCarro
{
    public class DeleteModel : PageModel
    {
        private readonly ModelosCarrosApiService _modelosCarrosApiService;

        public string Message { get; set; }

        public DeleteModel(ModelosCarrosApiService modelosCarrosApiService)
        {
            _modelosCarrosApiService = modelosCarrosApiService;
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
            }
            else
            {
                Message = message ?? "Error al obtener el modelo de carro desde la API.";
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

            var (success, message) = await _modelosCarrosApiService.EliminarModeloCarroAsync(id.Value);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = message ?? "Error al obtener el modelo de carro desde la API.";
                return Page();
            }
        }
    }
}
