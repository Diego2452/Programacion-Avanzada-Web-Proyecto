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
    public class TipoFinanciamientosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public TipoFinanciamientosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/TipoFinanciamientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoFinanciamientos>>> GetTipoFinanciamientos()
        {
          if (_context.TipoFinanciamientos == null)
          {
              return NotFound();
          }
            return await _context.TipoFinanciamientos.ToListAsync();
        }

        // GET: api/TipoFinanciamientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoFinanciamientos>> GetTipoFinanciamientos(int id)
        {
          if (_context.TipoFinanciamientos == null)
          {
              return NotFound();
          }
            var tipoFinanciamientos = await _context.TipoFinanciamientos.FindAsync(id);

            if (tipoFinanciamientos == null)
            {
                return NotFound();
            }

            return tipoFinanciamientos;
        }

        // PUT: api/TipoFinanciamientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoFinanciamientos(int id, TipoFinanciamientos tipoFinanciamientos)
        {
            if (id != tipoFinanciamientos.IdFinanciamiento)
            {
                return BadRequest();
            }

            _context.Entry(tipoFinanciamientos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoFinanciamientosExists(id))
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

        // POST: api/TipoFinanciamientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoFinanciamientos>> PostTipoFinanciamientos(TipoFinanciamientos tipoFinanciamientos)
        {
          if (_context.TipoFinanciamientos == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.TipoFinanciamientos'  is null.");
          }
            _context.TipoFinanciamientos.Add(tipoFinanciamientos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoFinanciamientos", new { id = tipoFinanciamientos.IdFinanciamiento }, tipoFinanciamientos);
        }

        // DELETE: api/TipoFinanciamientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoFinanciamientos(int id)
        {
            if (_context.TipoFinanciamientos == null)
            {
                return NotFound();
            }
            var tipoFinanciamientos = await _context.TipoFinanciamientos.FindAsync(id);
            if (tipoFinanciamientos == null)
            {
                return NotFound();
            }

            _context.TipoFinanciamientos.Remove(tipoFinanciamientos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoFinanciamientosExists(int id)
        {
            return (_context.TipoFinanciamientos?.Any(e => e.IdFinanciamiento == id)).GetValueOrDefault();
        }
    }
}
