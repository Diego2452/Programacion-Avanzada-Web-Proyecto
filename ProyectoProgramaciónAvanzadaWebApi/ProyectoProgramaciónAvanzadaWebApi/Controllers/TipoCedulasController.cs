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
    public class TipoCedulasController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public TipoCedulasController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/TipoCedulas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCedulas>>> GetTipoCedula()
        {
          if (_context.TipoCedulas == null)
          {
              return NotFound();
          }
            return await _context.TipoCedulas.ToListAsync();
        }

        // GET: api/TipoCedulas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCedulas>> GetTipoCedulas(int id)
        {
          if (_context.TipoCedulas == null)
          {
              return NotFound();
          }
            var tipoCedulas = await _context.TipoCedulas.FindAsync(id);

            if (tipoCedulas == null)
            {
                return NotFound();
            }

            return tipoCedulas;
        }

        // PUT: api/TipoCedulas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoCedulas(int id, TipoCedulas tipoCedulas)
        {
            if (id != tipoCedulas.IdTipoCedula)
            {
                return BadRequest();
            }

            _context.Entry(tipoCedulas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCedulasExists(id))
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

        // POST: api/TipoCedulas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoCedulas>> PostTipoCedulas(TipoCedulas tipoCedulas)
        {
          if (_context.TipoCedulas == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.TipoCedula'  is null.");
          }
            _context.TipoCedulas.Add(tipoCedulas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoCedulas", new { id = tipoCedulas.IdTipoCedula }, tipoCedulas);
        }

        // DELETE: api/TipoCedulas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoCedulas(int id)
        {
            if (_context.TipoCedulas == null)
            {
                return NotFound();
            }
            var tipoCedulas = await _context.TipoCedulas.FindAsync(id);
            if (tipoCedulas == null)
            {
                return NotFound();
            }

            _context.TipoCedulas.Remove(tipoCedulas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoCedulasExists(int id)
        {
            return (_context.TipoCedulas?.Any(e => e.IdTipoCedula == id)).GetValueOrDefault();
        }
    }
}
