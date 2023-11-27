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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoTransmision
{
    public class DeleteModel : PageModel
    {
        private readonly TipoTransmisionesApiService _tipoTransmisionesApiService;

        public string Message { get; set; }

        public DeleteModel(TipoTransmisionesApiService tipoTransmisionesApiService)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (success, message) = await _tipoTransmisionesApiService.EliminarTipoTransmisionAsync(id.Value);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = message ?? "Error al obtener el tipo de transmision desde la API.";
                return Page();
            }
        }
    }
}
