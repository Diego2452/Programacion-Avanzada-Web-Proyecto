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
    public class DetailsModel : PageModel
    {
        private readonly ProductosApiService _productosApiService;
        private readonly ImagenesProductosApiService _imagenesProductosApiService;

        public string Message { get; set; }

        public DetailsModel(
            ProductosApiService productoApiService,
            ImagenesProductosApiService imagenesProductosApiService)
        {
            _productosApiService = productoApiService;
            _imagenesProductosApiService = imagenesProductosApiService;
        }

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
            }
            else
            {
                Message = message ?? "Error al obtener el producto desde la API.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int imagenId, int productoId)
        {
            var (success, message) = await _imagenesProductosApiService.EliminarImagenProductoAsync(imagenId);

            if (success)
            {
                Message = message;
            }
            else
            {
                Message = $"Error al eliminar la imagen del producto: {message}";
            }

            return RedirectToPage(new { id = productoId });
        }

        public async Task<IActionResult> OnPostAgregarImagenAsync(string rutaImagen, int idProducto)
        {
            var imagen = new ImagenesProductos { ImagenPath = rutaImagen, IdProducto = idProducto };

            var (success, message) = await _imagenesProductosApiService.CrearImagenProductoAsync(imagen);

            if (success)
            {
                Message = message;
            }
            else
            {
                Message = $"Error al crear la imagen del producto: {message}";
            }

            return RedirectToPage(new { id = idProducto });
        }
    }
}
