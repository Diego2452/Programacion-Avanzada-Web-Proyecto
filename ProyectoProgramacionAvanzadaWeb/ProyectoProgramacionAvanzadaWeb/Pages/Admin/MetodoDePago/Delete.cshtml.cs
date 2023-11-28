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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.MetodoDePago
{
    public class DeleteModel : PageModel
    {
        private readonly MetodosDePagoApiService _metodosDePagoApiService;

        public string Message { get; set; }

        public DeleteModel(MetodosDePagoApiService metodosDePagoApiService)
        {
            _metodosDePagoApiService = metodosDePagoApiService;
        }

        [BindProperty]
      public MetodosDePago MetodosDePago { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (metodosDePago, message) = await _metodosDePagoApiService.ObtenerDetallesMetodoDePagoAsync(id.Value);

            if (metodosDePago != null)
            {
                MetodosDePago = metodosDePago;
            }
            else
            {
                Message = message ?? "Error al obtener el metodo de pago desde la API.";
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

            var (success, message) = await _metodosDePagoApiService.EliminarMetodoDePagoAsync(id.Value);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = message ?? "Error al obtener el metodo de pago desde la API.";
                return Page();
            }
        }
    }
}
