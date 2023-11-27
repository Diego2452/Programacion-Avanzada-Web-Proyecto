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
    public class IndexModel : PageModel
    {
        private readonly ProveedoresApiService _proveedoresApiService;

        public string Message { get; set; }

        public IndexModel(ProveedoresApiService proveedoresApiService)
        {
            _proveedoresApiService = proveedoresApiService;
        }

        public IList<Proveedores> Proveedores { get;set; } = new List<Proveedores>();

        public async Task OnGetAsync()
        {
            var (proveedores, errorMessage) = await _proveedoresApiService.ObtenerProveedoresAsync();

            if (proveedores != null)
            {
                Proveedores = proveedores;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener proveedores desde la API.";
            }
        }
    }
}
