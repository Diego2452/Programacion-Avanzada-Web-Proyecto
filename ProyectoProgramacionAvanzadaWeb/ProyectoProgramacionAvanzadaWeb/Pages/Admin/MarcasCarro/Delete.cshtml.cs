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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.MarcasCarro
{
    public class DeleteModel : PageModel
    {
        private readonly MarcasCarrosApiService _marcasCarrosApiService;

        public string Message { get; set; }

        public DeleteModel(MarcasCarrosApiService marcasCarrosApiService)
        {
            _marcasCarrosApiService = marcasCarrosApiService;
        }

        [BindProperty]
      public MarcasCarros MarcasCarros { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (marcaCarro, message) = await _marcasCarrosApiService.ObtenerDetallesMarcaCarroAsync(id.Value);

            if (marcaCarro != null)
            {
                MarcasCarros = marcaCarro;
            }
            else
            {
                Message = message ?? "Error al obtener la marca de carro desde la API.";
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

            var (success, message) = await _marcasCarrosApiService.EliminarMarcaCarroAsync(id.Value);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = message ?? "Error al obtener la marca de carro desde la API.";
                return Page();
            }
        }
    }
}
