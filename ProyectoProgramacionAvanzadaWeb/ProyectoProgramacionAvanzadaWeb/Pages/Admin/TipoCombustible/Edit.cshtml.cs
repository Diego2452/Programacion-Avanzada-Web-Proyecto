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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoCombustible
{
    public class EditModel : PageModel
    {
        private readonly TipoCombustiblesApiService _tipoCombustibleApiService;

        public string Message { get; set; }

        public EditModel(TipoCombustiblesApiService tipoCombustibleApiService)
        {
            _tipoCombustibleApiService = tipoCombustibleApiService;
        }

        [BindProperty]
        public TipoCombustibles TipoCombustibles { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (tipoCombustible, message) = await _tipoCombustibleApiService.ObtenerDetallesTipoCombustibleAsync(id.Value);

            if (tipoCombustible != null)
            {
                TipoCombustibles = tipoCombustible;
            }
            else
            {
                Message = message ?? "Error al obtener el tipo combustible desde la API.";
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

            var (success, message) = await _tipoCombustibleApiService.EditarTipoCombustibleAsync(TipoCombustibles);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Edit", new { id = TipoCombustibles.IdCombustible });
            }
            else
            {
                Message = message ?? "Error al actualizar el tipo de combustible.";
                return Page();
            }
        }
    }
}
