using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Carro
{
    public class DetailsModel : PageModel
    {
        private readonly CarrosApiService _carroApiService;
        private readonly CarrosImagenesApiService _carroImagenesApiService;

        public string Message { get; set; }

        public DetailsModel(
            CarrosApiService carroApiService,
            CarrosImagenesApiService carroImagenesApiService)
        {
            _carroApiService = carroApiService;
            _carroImagenesApiService = carroImagenesApiService;
        }

        public Carros Carros { get; set; } = new Carros(); 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (carro, message) = await _carroApiService.ObtenerDetallesCarroAsync(id.Value);

            if (carro != null)
            {
                Carros = carro;
            }
            else
            {
                Message = message ?? "Error al obtener el carro desde la API.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int idImagen, int idCarro)
        {
            var (success, message) = await _carroImagenesApiService.EliminarCarroImagenAsync(idImagen);

            if (success)
            {
                Message = message;
            }
            else
            {
                Message = $"Error al eliminar la imagen del carro: {message}";
            }

            return RedirectToPage(new { id = idCarro });
        }

        public async Task<IActionResult> OnPostAgregarImagenAsync(string rutaImagen, int idCarro)
        {
            var imagen = new CarrosImagenes { ImagenPath = rutaImagen, IdCarro = idCarro };

            var (success, message) = await _carroImagenesApiService.CrearCarroImagenAsync(imagen);

            if (success)
            {
                Message = message;
            }
            else
            {
                Message = $"Error al crear la imagen del carro: {message}";
            }

            return RedirectToPage(new { id = idCarro });
        }
    }
}
