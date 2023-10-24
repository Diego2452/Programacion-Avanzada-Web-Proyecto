using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public CarrosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Carros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carros>>> GetCarros()
        {
            try
            {
                var carros = await _context.Carros.Include(c => c.ModeloCarro)
                                                  .Include(c => c.Combustible)
                                                  .Include(c => c.Transmision)
                                                  .Include(c => c.Financiamiento)
                                                  .ToListAsync();

                if (carros == null || carros.Count == 0)
                {
                    return NotFound("No se encontraron carros.");
                }

                return carros;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Carros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carros>> GetCarros(int id)
        {
            try
            {
                var carro = await _context.Carros.Include(c => c.ModeloCarro)
                                                .Include(c => c.Combustible)
                                                .Include(c => c.Transmision)
                                                .Include(c => c.Financiamiento)
                                                .FirstOrDefaultAsync(c => c.IdCarro == id);

                if (carro == null)
                {
                    return NotFound("Carro no encontrado.");
                }

                return carro;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Carros
        [HttpPost]
        public async Task<ActionResult<Carros>> PostCarro(Carros carro)
        {
            if (_context.Carros == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Carros' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Carros.Add(carro);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCarros", new { id = carro.IdCarro }, carro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Carros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarros(int id, Carros carro)
        {
            if (id != carro.IdCarro)
            {
                return BadRequest("El ID del carro no coincide con el carro proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(carro).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Carro actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarroExists(id))
                {
                    return NotFound("Carro no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el carro.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Carros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarros(int id)
        {
            try
            {
                var carro = await _context.Carros.FindAsync(id);
                if (carro == null)
                {
                    return NotFound("Carro no encontrado.");
                }

                _context.Carros.Remove(carro);
                await _context.SaveChangesAsync();
                return Ok("Carro borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool CarroExists(int id)
        {
            return _context.Carros.Any(e => e.IdCarro == id);
        }
    }
}