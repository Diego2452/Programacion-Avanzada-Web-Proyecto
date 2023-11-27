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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Producto
{
    public class DeleteModel : PageModel
    {
        private readonly ProductosApiService _productosApiService;

        public string Message { get; set; }

        public DeleteModel(ProductosApiService productoApiService)
        {
            _productosApiService = productoApiService;
        }

        [BindProperty]
        public Productos Productos { get; set; } = new Productos();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (producto, message) = await _productosApiService.ObtenerDetallesProductoAsync(id.Value);

            if (producto != null)
            {
                Productos = producto;
            }
            else
            {
                Message = message ?? "Error al obtener el producto desde la API.";
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

            var (success, message) = await _productosApiService.EliminarProductoAsync(id.Value);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = message ?? "Error al obtener el producto desde la API.";
                return Page();
            }
        }
    }
}
