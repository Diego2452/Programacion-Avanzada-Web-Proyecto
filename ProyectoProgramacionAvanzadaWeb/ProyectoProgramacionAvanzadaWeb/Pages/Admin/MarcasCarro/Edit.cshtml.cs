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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.MarcasCarro
{
    public class EditModel : PageModel
    {
        private readonly MarcasCarrosApiService _marcasCarrosApiService;

        public string Message { get; set; }

        public EditModel(MarcasCarrosApiService marcasCarrosApiService)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (success, message) = await _marcasCarrosApiService.EditarMarcaCarroAsync(MarcasCarros);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Edit", new { id = MarcasCarros.IdMarca });
            }
            else
            {
                Message = message ?? "Error al actualizar la marca de carro.";
                return Page();
            }
        }

    }
}
