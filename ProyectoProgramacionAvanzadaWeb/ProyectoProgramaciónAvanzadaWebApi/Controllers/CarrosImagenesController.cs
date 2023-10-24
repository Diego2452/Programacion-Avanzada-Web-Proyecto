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
            try
            {
                var carrosImagenes = await _context.CarrosImagenes.Include(ci => ci.Carro).ToListAsync();

                if (carrosImagenes == null || carrosImagenes.Count == 0)
                {
                    return NotFound("No se encontraron imágenes de carros.");
                }

                return carrosImagenes;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/CarrosImagenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarrosImagenes>> GetCarrosImagenes(int id)
        {
            try
            {
                var imagenCarro = await _context.CarrosImagenes.Include(ci => ci.Carro).FirstOrDefaultAsync(ci => ci.IdImagen == id);

                if (imagenCarro == null)
                {
                    return NotFound("Imagen de carro no encontrada.");
                }

                return imagenCarro;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/CarrosImagenes
        [HttpPost]
        public async Task<ActionResult<CarrosImagenes>> PostCarroImagen(CarrosImagenes imagenCarro)
        {
            if (_context.CarrosImagenes == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.CarrosImagenes' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.CarrosImagenes.Add(imagenCarro);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCarrosImagenes", new { id = imagenCarro.IdImagen }, imagenCarro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/CarrosImagenes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrosImagenes(int id, CarrosImagenes imagenCarro)
        {
            if (id != imagenCarro.IdImagen)
            {
                return BadRequest("El ID de la imagen de carro no coincide con la imagen proporcionada.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(imagenCarro).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Imagen de carro actualizada exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrosImagenesExists(id))
                {
                    return NotFound("Imagen de carro no encontrada.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar la imagen de carro.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/CarrosImagenes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrosImagenes(int id)
        {
            try
            {
                var imagenCarro = await _context.CarrosImagenes.FindAsync(id);
                if (imagenCarro == null)
                {
                    return NotFound("Imagen de carro no encontrada.");
                }

                _context.CarrosImagenes.Remove(imagenCarro);
                await _context.SaveChangesAsync();
                return Ok("Imagen de carro borrada con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool CarrosImagenesExists(int id)
        {
            return _context.CarrosImagenes.Any(e => e.IdImagen == id);
        }
    }
}