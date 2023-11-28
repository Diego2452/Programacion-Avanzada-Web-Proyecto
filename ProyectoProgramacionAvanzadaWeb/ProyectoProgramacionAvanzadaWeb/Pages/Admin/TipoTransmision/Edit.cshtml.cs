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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoTransmision
{
    public class EditModel : PageModel
    {
        private readonly TipoTransmisionesApiService _tipoTransmisionesApiService;

        public string Message { get; set; }

        public EditModel(TipoTransmisionesApiService tipoTransmisionesApiService)
        {
            _tipoTransmisionesApiService = tipoTransmisionesApiService;
        }

        [BindProperty]
        public TipoTransmisiones TipoTransmisiones { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (tipoTransmisiones, message) = await _tipoTransmisionesApiService.ObtenerDetallesTipoTransmisionAsync(id.Value);

            if (tipoTransmisiones != null)
            {
                TipoTransmisiones = tipoTransmisiones;
            }
            else
            {
                Message = message ?? "Error al obtener el tipo de transmision desde la API.";
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

            var (success, message) = await _tipoTransmisionesApiService.EditarTipoTransmisionAsync(TipoTransmisiones);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Edit", new { id = TipoTransmisiones.IdTransmision });
            }
            else
            {
                Message = message ?? "Error al actualizar el tipo de transmision.";
                return Page();
            }
        }
    }
}
