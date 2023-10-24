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
    public class ImagenesProductosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public ImagenesProductosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/ImagenesProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenesProductos>>> GetImagenesProductos()
        {
            try
            {
                var imagenes = await _context.ImagenesProductos.Include(ip => ip.Producto).ToListAsync();

                if (imagenes == null || imagenes.Count == 0)
                {
                    return NotFound("No se encontraron imágenes de productos.");
                }

                return imagenes;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/ImagenesProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagenesProductos>> GetImagenesProductos(int id)
        {
            try
            {
                var imagen = await _context.ImagenesProductos
                    .Include(ip => ip.Producto)
                    .FirstOrDefaultAsync(ip => ip.IdImagen == id);

                if (imagen == null)
                {
                    return NotFound("Imagen de producto no encontrada.");
                }

                return imagen;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/ImagenesProductos
        [HttpPost]
        public async Task<ActionResult<ImagenesProductos>> PostImagenesProductos(ImagenesProductos imagen)
        {
            if (_context.ImagenesProductos == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.ImagenesProductos' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.ImagenesProductos.Add(imagen);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetImagenesProductos", new { id = imagen.IdImagen }, imagen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/ImagenesProductos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagenesProductos(int id, ImagenesProductos imagen)
        {
            if (id != imagen.IdImagen)
            {
                return BadRequest("El ID de imagen de producto no coincide con la imagen proporcionada.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(imagen).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Imagen de producto actualizada exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagenesProductosExists(id))
                {
                    return NotFound("Imagen de producto no encontrada.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar la imagen de producto.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/ImagenesProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagenesProductos(int id)
        {
            try
            {
                var imagen = await _context.ImagenesProductos.FindAsync(id);
                if (imagen == null)
                {
                    return NotFound("Imagen de producto no encontrada.");
                }

                _context.ImagenesProductos.Remove(imagen);
                await _context.SaveChangesAsync();
                return Ok("Imagen de producto borrada con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool ImagenesProductosExists(int id)
        {
            return _context.ImagenesProductos.Any(ip => ip.IdImagen == id);
        }
    }
}
