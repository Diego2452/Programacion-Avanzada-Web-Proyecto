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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.ModelosCarro
{
    public class IndexModel : PageModel
    {
        private readonly ModelosCarrosApiService _modelosCarrosApiService;

        public string Message { get; set; }

        public IndexModel(ModelosCarrosApiService modelosCarrosApiService)
        {
            _modelosCarrosApiService = modelosCarrosApiService;
        }

        public IList<ModelosCarros> ModelosCarros { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var (modelosCarros, errorMessage) = await _modelosCarrosApiService.ObtenerModelosCarrosAsync();

            if (modelosCarros != null)
            {
                ModelosCarros = modelosCarros;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener los modelos de carros desde la API.";
            }
        }
    }
}
