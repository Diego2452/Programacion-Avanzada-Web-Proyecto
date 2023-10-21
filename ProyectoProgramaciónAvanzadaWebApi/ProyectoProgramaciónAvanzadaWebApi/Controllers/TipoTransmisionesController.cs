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
    public class TipoTransmisionesController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public TipoTransmisionesController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/TipoTransmisiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTransmisiones>>> GetTipoTransmisiones()
        {
          if (_context.TipoTransmisiones == null)
          {
              return NotFound();
          }
            return await _context.TipoTransmisiones.ToListAsync();
        }

        // GET: api/TipoTransmisiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTransmisiones>> GetTipoTransmisiones(int id)
        {
          if (_context.TipoTransmisiones == null)
          {
              return NotFound();
          }
            var tipoTransmisiones = await _context.TipoTransmisiones.FindAsync(id);

            if (tipoTransmisiones == null)
            {
                return NotFound();
            }

            return tipoTransmisiones;
        }

        // PUT: api/TipoTransmisiones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoTransmisiones(int id, TipoTransmisiones tipoTransmisiones)
        {
            if (id != tipoTransmisiones.IdTransmision)
            {
                return BadRequest();
            }

            _context.Entry(tipoTransmisiones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoTransmisionesExists(id))
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

        // POST: api/TipoTransmisiones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoTransmisiones>> PostTipoTransmisiones(TipoTransmisiones tipoTransmisiones)
        {
          if (_context.TipoTransmisiones == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.TipoTransmisiones'  is null.");
          }
            _context.TipoTransmisiones.Add(tipoTransmisiones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoTransmisiones", new { id = tipoTransmisiones.IdTransmision }, tipoTransmisiones);
        }

        // DELETE: api/TipoTransmisiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoTransmisiones(int id)
        {
            if (_context.TipoTransmisiones == null)
            {
                return NotFound();
            }
            var tipoTransmisiones = await _context.TipoTransmisiones.FindAsync(id);
            if (tipoTransmisiones == null)
            {
                return NotFound();
            }

            _context.TipoTransmisiones.Remove(tipoTransmisiones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoTransmisionesExists(int id)
        {
            return (_context.TipoTransmisiones?.Any(e => e.IdTransmision == id)).GetValueOrDefault();
        }
    }
}
