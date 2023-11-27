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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Proveedor
{
    public class EditModel : PageModel
    {
        private readonly ProveedoresApiService _proveedoresApiService;

        public string Message { get; set; }

        public EditModel(ProveedoresApiService proveedoresApiService)
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
                Message = message ?? "Error al obtener el proveedor desde la API.";
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

            var (success, message) = await _proveedoresApiService.EditarProveedorAsync(Proveedores);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Edit", new { id = Proveedores.IdProveedor });
            }
            else
            {
                Message = message ?? "Error al actualizar el proveedor.";
                return Page();
            }
        }
    }
}
