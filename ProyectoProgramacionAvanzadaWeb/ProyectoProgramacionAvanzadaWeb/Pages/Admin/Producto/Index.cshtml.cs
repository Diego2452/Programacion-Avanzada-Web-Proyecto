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
    public class IndexModel : PageModel
    {
        private readonly ProductosApiService _productosApiService;

        public string Message { get; set; }

        public IndexModel(ProductosApiService productoApiService)
        {
            _productosApiService = productoApiService;
        }

        public IList<Productos> Productos { get;set; } = new List<Productos>();

        public async Task OnGetAsync()
        {
            var (productos, errorMessage) = await _productosApiService.ObtenerProductosAsync();

            if (productos != null)
            {
                Productos = productos;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener categorias desde la API.";
            }
        }
    }
}
