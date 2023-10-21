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
    public class MetodosDePagosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public MetodosDePagosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/MetodosDePagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodosDePago>>> GetMetodosDePago()
        {
          if (_context.MetodosDePago == null)
          {
              return NotFound();
          }
            return await _context.MetodosDePago.ToListAsync();
        }

        // GET: api/MetodosDePagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetodosDePago>> GetMetodosDePago(int id)
        {
          if (_context.MetodosDePago == null)
          {
              return NotFound();
          }
            var metodosDePago = await _context.MetodosDePago.FindAsync(id);

            if (metodosDePago == null)
            {
                return NotFound();
            }

            return metodosDePago;
        }

        // PUT: api/MetodosDePagos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodosDePago(int id, MetodosDePago metodosDePago)
        {
            if (id != metodosDePago.IdMetodoPago)
            {
                return BadRequest();
            }

            _context.Entry(metodosDePago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodosDePagoExists(id))
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

        // POST: api/MetodosDePagos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MetodosDePago>> PostMetodosDePago(MetodosDePago metodosDePago)
        {
          if (_context.MetodosDePago == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.MetodosDePago'  is null.");
          }
            _context.MetodosDePago.Add(metodosDePago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetodosDePago", new { id = metodosDePago.IdMetodoPago }, metodosDePago);
        }

        // DELETE: api/MetodosDePagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodosDePago(int id)
        {
            if (_context.MetodosDePago == null)
            {
                return NotFound();
            }
            var metodosDePago = await _context.MetodosDePago.FindAsync(id);
            if (metodosDePago == null)
            {
                return NotFound();
            }

            _context.MetodosDePago.Remove(metodosDePago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetodosDePagoExists(int id)
        {
            return (_context.MetodosDePago?.Any(e => e.IdMetodoPago == id)).GetValueOrDefault();
        }
    }
}
