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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Producto
{
    public class EditModel : PageModel
    {
        private readonly ProductosApiService _productosApiService;
        private readonly CategoriaApiService _categoriaApiService;
        private readonly ProveedoresApiService _proveedoresApiService;

        public string Message { get; set; }

        public EditModel(
            ProductosApiService productoApiService,
            CategoriaApiService categoriaApiService,
            ProveedoresApiService proveedoresApiService
            )
        {
            _productosApiService = productoApiService;
            _categoriaApiService = categoriaApiService;
            _proveedoresApiService = proveedoresApiService;
        }

        [BindProperty]
        public Productos Productos { get; set; } = default!;

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
                await SelectLists();
            }
            else
            {
                Message = message ?? "Error al obtener el producto desde la API.";
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SelectLists();
                return Page();
            }

            var (success, message) = await _productosApiService.EditarProductoAsync(Productos);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Edit", new { id = Productos.IdProducto });
            }
            else
            {
                Message = message ?? "Error al actualizar el producto.";
                await SelectLists();
                return Page();
            }
        }

        private async Task SelectLists()
        {
            var (categoria, _) = await _categoriaApiService.ObtenerCategoriasAsync();
            ViewData["IdCategoria"] = categoria != null ? new SelectList(categoria, "IdCategoria", "TipoCategoria") : null;
            var (proveedores, _) = await _proveedoresApiService.ObtenerProveedoresAsync();
            ViewData["IdProveedor"] = proveedores != null ? new SelectList(proveedores, "IdProveedor", "NombreProveedor") : null;
        }
    }
}
