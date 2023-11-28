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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Categorias
{
    public class DetailsModel : PageModel
    {
        private readonly CategoriaApiService _categoriaApiService;

        public string Message { get; set; }

        public DetailsModel(CategoriaApiService categoriaApiService)
        {
            _categoriaApiService = categoriaApiService;
        }

        public Categoria Categoria { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (categoria, message) = await _categoriaApiService.ObtenerDetallesCategoriaAsync(id.Value);

            if (categoria != null)
            {
                Categoria = categoria;
            }
            else
            {
                Message = message ?? "Error al obtener la categoria desde la API.";
            }

            return Page();
        }
    }
}
