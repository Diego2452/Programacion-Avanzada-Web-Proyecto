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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Carro
{
    public class EditModel : PageModel
    {
        private readonly ProyectoProgramacionAvanzadaWeb.Data.ProyectoProgramacionAvanzadaWebContext _context;

        public EditModel(ProyectoProgramacionAvanzadaWeb.Data.ProyectoProgramacionAvanzadaWebContext context)
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

            var carros =  await _context.Carros.FirstOrDefaultAsync(m => m.IdCarro == id);
            if (carros == null)
            {
                return NotFound();
            }
            Carros = carros;
           ViewData["IdCombustible"] = new SelectList(_context.Set<TipoCombustibles>(), "IdCombustible", "TipoCombustible");
           ViewData["IdFinanciamiento"] = new SelectList(_context.Set<TipoFinanciamientos>(), "IdFinanciamiento", "TipoFinanciamiento");
           ViewData["IdModelo"] = new SelectList(_context.Set<ModelosCarros>(), "IdModelo", "Modelo");
           ViewData["IdTransmision"] = new SelectList(_context.Set<TipoTransmisiones>(), "IdTransmision", "TipoTransmision");
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

            _context.Attach(Carros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrosExists(Carros.IdCarro))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CarrosExists(int id)
        {
          return (_context.Carros?.Any(e => e.IdCarro == id)).GetValueOrDefault();
        }
    }
}
