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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Proveedor
{
    public class DeleteModel : PageModel
    {
        private readonly ProveedoresApiService _proveedoresApiService;

        public string Message { get; set; }

        public DeleteModel(ProveedoresApiService proveedoresApiService)
        {
            _proveedoresApiService = proveedoresApiService;
        }

        [BindProperty]
        public Proveedores Proveedores { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (proveedor, message) = await _proveedoresApiService.ObtenerDetallesProveedorAsync(id.Value);

            if (proveedor != null)
            {
                Proveedores = proveedor;
            }
            else
            {
                Message = message ?? "Error al obtener la categoria desde la API.";
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

            var (success, message) = await _proveedoresApiService.EliminarProveedorAsync(id.Value);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = message ?? "Error al obtener el proveedor desde la API.";
                return Page();
            }
        }
    }
}
