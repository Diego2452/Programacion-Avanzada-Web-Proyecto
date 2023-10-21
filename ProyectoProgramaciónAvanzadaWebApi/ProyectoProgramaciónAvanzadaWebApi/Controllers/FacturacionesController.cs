using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturacionesController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public FacturacionesController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Facturacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facturacion>>> GetFacturacion()
        {
          if (_context.Facturacion == null)
          {
              return NotFound();
          }
            return await _context.Facturacion.ToListAsync();
        }

        // GET: api/Facturacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facturacion>> GetFacturacion(int id)
        {
          if (_context.Facturacion == null)
          {
              return NotFound();
          }
            var facturacion = await _context.Facturacion.FindAsync(id);

            if (facturacion == null)
            {
                return NotFound();
            }

            return facturacion;
        }

        // PUT: api/Facturacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturacion(int id, Facturacion facturacion)
        {
            if (id != facturacion.IdFactura)
            {
                return BadRequest();
            }

            _context.Entry(facturacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Facturacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facturacion>> PostFacturacion(Facturacion facturacion)
        {
          if (_context.Facturacion == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.Facturacion'  is null.");
          }
            _context.Facturacion.Add(facturacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacturacion", new { id = facturacion.IdFactura }, facturacion);
        }

        // DELETE: api/Facturacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturacion(int id)
        {
            if (_context.Facturacion == null)
            {
                return NotFound();
            }
            var facturacion = await _context.Facturacion.FindAsync(id);
            if (facturacion == null)
            {
                return NotFound();
            }

            _context.Facturacion.Remove(facturacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturacionExists(int id)
        {
            return (_context.Facturacion?.Any(e => e.IdFactura == id)).GetValueOrDefault();
        }
    }
}
