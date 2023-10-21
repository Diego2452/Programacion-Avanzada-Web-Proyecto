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
    public class TipoCombustiblesController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public TipoCombustiblesController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/TipoCombustibles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCombustibles>>> GetTipoCombustibles()
        {
          if (_context.TipoCombustibles == null)
          {
              return NotFound();
          }
            return await _context.TipoCombustibles.ToListAsync();
        }

        // GET: api/TipoCombustibles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCombustibles>> GetTipoCombustibles(int id)
        {
          if (_context.TipoCombustibles == null)
          {
              return NotFound();
          }
            var tipoCombustibles = await _context.TipoCombustibles.FindAsync(id);

            if (tipoCombustibles == null)
            {
                return NotFound();
            }

            return tipoCombustibles;
        }

        // PUT: api/TipoCombustibles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoCombustibles(int id, TipoCombustibles tipoCombustibles)
        {
            if (id != tipoCombustibles.IdCombustible)
            {
                return BadRequest();
            }

            _context.Entry(tipoCombustibles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCombustiblesExists(id))
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

        // POST: api/TipoCombustibles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoCombustibles>> PostTipoCombustibles(TipoCombustibles tipoCombustibles)
        {
          if (_context.TipoCombustibles == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.TipoCombustibles'  is null.");
          }
            _context.TipoCombustibles.Add(tipoCombustibles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoCombustibles", new { id = tipoCombustibles.IdCombustible }, tipoCombustibles);
        }

        // DELETE: api/TipoCombustibles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoCombustibles(int id)
        {
            if (_context.TipoCombustibles == null)
            {
                return NotFound();
            }
            var tipoCombustibles = await _context.TipoCombustibles.FindAsync(id);
            if (tipoCombustibles == null)
            {
                return NotFound();
            }

            _context.TipoCombustibles.Remove(tipoCombustibles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoCombustiblesExists(int id)
        {
            return (_context.TipoCombustibles?.Any(e => e.IdCombustible == id)).GetValueOrDefault();
        }
    }
}
