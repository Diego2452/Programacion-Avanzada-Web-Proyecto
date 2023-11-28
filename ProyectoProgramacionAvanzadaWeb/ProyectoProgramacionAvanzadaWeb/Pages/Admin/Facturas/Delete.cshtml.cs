using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Facturas
{
    public class DeleteModel : PageModel
    {
        private readonly ProyectoProgramacionAvanzadaWeb.Data.ProyectoProgramacionAvanzadaWebContext _context;

        public DeleteModel(ProyectoProgramacionAvanzadaWeb.Data.ProyectoProgramacionAvanzadaWebContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Facturacion Facturacion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Facturacion == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacion.FirstOrDefaultAsync(m => m.IdFactura == id);

            if (facturacion == null)
            {
                return NotFound();
            }
            else 
            {
                Facturacion = facturacion;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Facturacion == null)
            {
                return NotFound();
            }
            var facturacion = await _context.Facturacion.FindAsync(id);

            if (facturacion != null)
            {
                Facturacion = facturacion;
                _context.Facturacion.Remove(Facturacion);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
