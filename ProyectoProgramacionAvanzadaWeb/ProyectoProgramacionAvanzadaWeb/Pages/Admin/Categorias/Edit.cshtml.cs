using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;
using ProyectoProgramacionAvanzadaWeb.Services;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Categorias
{
    public class EditModel : PageModel
    {
        private readonly CategoriaApiService _categoriaApiService;

        public string Message { get; set; }

        public EditModel(CategoriaApiService categoriaApiService)
        {
            _categoriaApiService = categoriaApiService;
        }

        [BindProperty]
        public Categoria Categoria { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string accessToken = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Index");
            }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (success, message) = await _categoriaApiService.EditarCategoriaAsync(Categoria);

            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToPage("./Edit", new { id = Categoria.IdCategoria });
            }
            else
            {
                Message = message ?? "Error al actualizar la categoria.";
                return Page();
            }
        }
    }
}
