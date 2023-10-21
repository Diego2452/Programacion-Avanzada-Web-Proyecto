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
    public class CarrosImagenesController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public CarrosImagenesController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/CarrosImagenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrosImagenes>>> GetCarrosImagenes()
        {
          if (_context.CarrosImagenes == null)
          {
              return NotFound();
          }
            return await _context.CarrosImagenes.ToListAsync();
        }

        // GET: api/CarrosImagenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarrosImagenes>> GetCarrosImagenes(int id)
        {
          if (_context.CarrosImagenes == null)
          {
              return NotFound();
          }
            var carrosImagenes = await _context.CarrosImagenes.FindAsync(id);

            if (carrosImagenes == null)
            {
                return NotFound();
            }

            return carrosImagenes;
        }

        // PUT: api/CarrosImagenes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrosImagenes(int id, CarrosImagenes carrosImagenes)
        {
            if (id != carrosImagenes.IdImagen)
            {
                return BadRequest();
            }

            _context.Entry(carrosImagenes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrosImagenesExists(id))
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

        // POST: api/CarrosImagenes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarrosImagenes>> PostCarrosImagenes(CarrosImagenes carrosImagenes)
        {
          if (_context.CarrosImagenes == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.CarrosImagenes'  is null.");
          }
            _context.CarrosImagenes.Add(carrosImagenes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrosImagenes", new { id = carrosImagenes.IdImagen }, carrosImagenes);
        }

        // DELETE: api/CarrosImagenes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrosImagenes(int id)
        {
            if (_context.CarrosImagenes == null)
            {
                return NotFound();
            }
            var carrosImagenes = await _context.CarrosImagenes.FindAsync(id);
            if (carrosImagenes == null)
            {
                return NotFound();
            }

            _context.CarrosImagenes.Remove(carrosImagenes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrosImagenesExists(int id)
        {
            return (_context.CarrosImagenes?.Any(e => e.IdImagen == id)).GetValueOrDefault();
        }
    }
}
