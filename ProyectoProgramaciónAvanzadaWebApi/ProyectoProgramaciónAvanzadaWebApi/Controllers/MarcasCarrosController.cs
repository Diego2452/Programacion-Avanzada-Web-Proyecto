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
    public class MarcasCarrosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public MarcasCarrosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/MarcasCarros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcasCarros>>> GetMarcasCarros()
        {
          if (_context.MarcasCarros == null)
          {
              return NotFound();
          }
            return await _context.MarcasCarros.ToListAsync();
        }

        // GET: api/MarcasCarros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarcasCarros>> GetMarcasCarros(int id)
        {
          if (_context.MarcasCarros == null)
          {
              return NotFound();
          }
            var marcasCarros = await _context.MarcasCarros.FindAsync(id);

            if (marcasCarros == null)
            {
                return NotFound();
            }

            return marcasCarros;
        }

        // PUT: api/MarcasCarros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarcasCarros(int id, MarcasCarros marcasCarros)
        {
            if (id != marcasCarros.IdMarca)
            {
                return BadRequest();
            }

            _context.Entry(marcasCarros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcasCarrosExists(id))
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

        // POST: api/MarcasCarros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MarcasCarros>> PostMarcasCarros(MarcasCarros marcasCarros)
        {
          if (_context.MarcasCarros == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.MarcasCarros'  is null.");
          }
            _context.MarcasCarros.Add(marcasCarros);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarcasCarros", new { id = marcasCarros.IdMarca }, marcasCarros);
        }

        // DELETE: api/MarcasCarros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarcasCarros(int id)
        {
            if (_context.MarcasCarros == null)
            {
                return NotFound();
            }
            var marcasCarros = await _context.MarcasCarros.FindAsync(id);
            if (marcasCarros == null)
            {
                return NotFound();
            }

            _context.MarcasCarros.Remove(marcasCarros);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarcasCarrosExists(int id)
        {
            return (_context.MarcasCarros?.Any(e => e.IdMarca == id)).GetValueOrDefault();
        }
    }
}
