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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoCombustible
{
    public class IndexModel : PageModel
    {
        private readonly TipoCombustiblesApiService _tipoCombustibleApiService;

        public string Message { get; set; }

        public IndexModel(TipoCombustiblesApiService tipoCombustibleApiService)
        {
            _tipoCombustibleApiService = tipoCombustibleApiService;
        }

        public IList<TipoCombustibles> TipoCombustibles { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var (tipoCombustible, errorMessage) = await _tipoCombustibleApiService.ObtenerTipoCombustiblesAsync();

            if (tipoCombustible != null)
            {
                TipoCombustibles = tipoCombustible;
            }
            else
            {
                Message = errorMessage ?? "Error al obtener los tipos de combustibles desde la API.";
            }
        }
    }
}
