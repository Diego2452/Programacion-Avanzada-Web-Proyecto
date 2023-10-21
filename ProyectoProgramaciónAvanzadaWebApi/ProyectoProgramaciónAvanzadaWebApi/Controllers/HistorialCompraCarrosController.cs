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
    public class HistorialCompraCarrosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public HistorialCompraCarrosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/HistorialCompraCarros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialCompraCarro>>> GetHistorialCompraCarro()
        {
          if (_context.HistorialCompraCarro == null)
          {
              return NotFound();
          }
            return await _context.HistorialCompraCarro.ToListAsync();
        }

        // GET: api/HistorialCompraCarros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialCompraCarro>> GetHistorialCompraCarro(int id)
        {
          if (_context.HistorialCompraCarro == null)
          {
              return NotFound();
          }
            var historialCompraCarro = await _context.HistorialCompraCarro.FindAsync(id);

            if (historialCompraCarro == null)
            {
                return NotFound();
            }

            return historialCompraCarro;
        }

        // PUT: api/HistorialCompraCarros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialCompraCarro(int id, HistorialCompraCarro historialCompraCarro)
        {
            if (id != historialCompraCarro.IdHistorialCompraCarro)
            {
                return BadRequest();
            }

            _context.Entry(historialCompraCarro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialCompraCarroExists(id))
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

        // POST: api/HistorialCompraCarros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistorialCompraCarro>> PostHistorialCompraCarro(HistorialCompraCarro historialCompraCarro)
        {
          if (_context.HistorialCompraCarro == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.HistorialCompraCarro'  is null.");
          }
            _context.HistorialCompraCarro.Add(historialCompraCarro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorialCompraCarro", new { id = historialCompraCarro.IdHistorialCompraCarro }, historialCompraCarro);
        }

        // DELETE: api/HistorialCompraCarros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialCompraCarro(int id)
        {
            if (_context.HistorialCompraCarro == null)
            {
                return NotFound();
            }
            var historialCompraCarro = await _context.HistorialCompraCarro.FindAsync(id);
            if (historialCompraCarro == null)
            {
                return NotFound();
            }

            _context.HistorialCompraCarro.Remove(historialCompraCarro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialCompraCarroExists(int id)
        {
            return (_context.HistorialCompraCarro?.Any(e => e.IdHistorialCompraCarro == id)).GetValueOrDefault();
        }
    }
}
