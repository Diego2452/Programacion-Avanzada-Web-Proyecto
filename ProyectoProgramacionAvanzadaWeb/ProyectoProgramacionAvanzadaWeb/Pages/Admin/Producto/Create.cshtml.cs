using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Producto
{
    public class CreateModel : PageModel
    {
        private readonly ProductosApiService _productosApiService;
        private readonly CategoriaApiService _categoriaApiService;
        private readonly ProveedoresApiService _proveedoresApiService;

        public string Message { get; set; }

        public CreateModel(
            ProductosApiService productoApiService,
            CategoriaApiService categoriaApiService,
            ProveedoresApiService proveedoresApiService
            )
        {
            _productosApiService = productoApiService;
            _categoriaApiService = categoriaApiService;
            _proveedoresApiService = proveedoresApiService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await SelectLists();
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error interno del servidor: " + ex.Message;
                return Page();
            }
        }

        [BindProperty]
        public Productos Productos { get; set; } = new Productos();
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SelectLists();
                return Page();
            }

            var (success, message) = await _productosApiService.CrearProductoAsync(Productos);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Create");
            }
            else
            {
                Message = message ?? "Error el producto.";
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
