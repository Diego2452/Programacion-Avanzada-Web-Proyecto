﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.HistorialComprasCarros
{
    public class DeleteModel : PageModel
    {
        private readonly HistorialCompraCarroApiService _historialCompraCarroApiService;

        public string Message { get; set; }

        public DeleteModel(HistorialCompraCarroApiService historialCompraCarroApiService)
        {
            _historialCompraCarroApiService = historialCompraCarroApiService;
        }

        [BindProperty]
      public HistorialCompraCarro HistorialCompraCarro { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (historialCompraCarro, message) = await _historialCompraCarroApiService.ObtenerDetallesHistorialAsync(id.Value);

            if (historialCompraCarro != null)
            {
                HistorialCompraCarro = historialCompraCarro;
            }
            else
            {
                Message = message ?? "Error al obtener el historial de compra de carro desde la API.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            var (success, message) = await _historialCompraCarroApiService.EliminarHistorialAsync(id.Value);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = message ?? "Error al obtener el historial de compra de carro desde la API.";
                return Page();
            }
        }
    }
}
