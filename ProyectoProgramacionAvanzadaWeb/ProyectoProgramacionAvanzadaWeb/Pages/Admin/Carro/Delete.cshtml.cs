using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Carro
{
    public class DeleteModel : PageModel
    {
        private readonly ProyectoProgramacionAvanzadaWeb.Data.ProyectoProgramacionAvanzadaWebContext _context;

        public DeleteModel(ProyectoProgramacionAvanzadaWeb.Data.ProyectoProgramacionAvanzadaWebContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Carros Carros { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Carros == null)
            {
                return NotFound();
            }

            var carros = await _context.Carros.FirstOrDefaultAsync(m => m.IdCarro == id);

            if (carros == null)
            {
                return NotFound();
            }
            else 
            {
                Carros = carros;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Carros == null)
            {
                return NotFound();
            }
            var carros = await _context.Carros.FindAsync(id);

            if (carros != null)
            {
                Carros = carros;
                _context.Carros.Remove(Carros);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
