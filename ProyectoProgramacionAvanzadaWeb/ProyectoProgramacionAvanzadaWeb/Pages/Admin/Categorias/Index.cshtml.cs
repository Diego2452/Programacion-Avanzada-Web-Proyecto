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
    public class IndexModel : PageModel
    {
        private readonly CategoriaApiService _categoriaApiService;

        public string Message { get; set; }

        public IndexModel(CategoriaApiService categoriaApiService)
        {
            _categoriaApiService = categoriaApiService;
        }

        public IList<Categoria> Categoria { get;set; } = new List<Categoria>();

        public async Task OnGetAsync()
        {
            var (categorias, errorMessage) = await _categoriaApiService.ObtenerCategoriasAsync();

            if (categorias != null)
            {
                Categoria = categorias;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener categorias desde la API.";
            }
        }
    }
}
