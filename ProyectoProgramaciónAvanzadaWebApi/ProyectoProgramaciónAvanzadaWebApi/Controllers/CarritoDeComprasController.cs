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
    public class CarritoDeComprasController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public CarritoDeComprasController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/CarritoDeCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarritoDeCompras>>> GetCarritoDeCompras()
        {
          if (_context.CarritoDeCompras == null)
          {
              return NotFound();
          }
            return await _context.CarritoDeCompras.ToListAsync();
        }

        // GET: api/CarritoDeCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDeCompras>> GetCarritoDeCompras(int id)
        {
          if (_context.CarritoDeCompras == null)
          {
              return NotFound();
          }
            var carritoDeCompras = await _context.CarritoDeCompras.FindAsync(id);

            if (carritoDeCompras == null)
            {
                return NotFound();
            }

            return carritoDeCompras;
        }

        // PUT: api/CarritoDeCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarritoDeCompras(int id, CarritoDeCompras carritoDeCompras)
        {
            if (id != carritoDeCompras.IdCarrito)
            {
                return BadRequest();
            }

            _context.Entry(carritoDeCompras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoDeComprasExists(id))
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

        // POST: api/CarritoDeCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarritoDeCompras>> PostCarritoDeCompras(CarritoDeCompras carritoDeCompras)
        {
          if (_context.CarritoDeCompras == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.CarritoDeCompras'  is null.");
          }
            _context.CarritoDeCompras.Add(carritoDeCompras);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarritoDeCompras", new { id = carritoDeCompras.IdCarrito }, carritoDeCompras);
        }

        // DELETE: api/CarritoDeCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarritoDeCompras(int id)
        {
            if (_context.CarritoDeCompras == null)
            {
                return NotFound();
            }
            var carritoDeCompras = await _context.CarritoDeCompras.FindAsync(id);
            if (carritoDeCompras == null)
            {
                return NotFound();
            }

            _context.CarritoDeCompras.Remove(carritoDeCompras);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarritoDeComprasExists(int id)
        {
            return (_context.CarritoDeCompras?.Any(e => e.IdCarrito == id)).GetValueOrDefault();
        }
    }
}
