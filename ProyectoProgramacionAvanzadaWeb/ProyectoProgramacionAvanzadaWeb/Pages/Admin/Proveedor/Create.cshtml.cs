using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Proveedor
{
    public class CreateModel : PageModel
    {
        private readonly ProveedoresApiService _proveedoresApiService;

        public string Message { get; set; }

        public CreateModel(ProveedoresApiService proveedoresApiService)
        {
            _proveedoresApiService = proveedoresApiService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Proveedores Proveedores { get; set; } = new Proveedores();
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (success, message) = await _proveedoresApiService.CrearProveedorAsync(Proveedores);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Create");
            }
            else
            {
                Message = message ?? "Error al crear el proveedor.";
                return Page();
            }
        }
    }
}
