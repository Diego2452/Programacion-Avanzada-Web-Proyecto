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
          if (_context.ImagenesProductos == null)
          {
              return NotFound();
          }
            return await _context.ImagenesProductos.ToListAsync();
        }

        // GET: api/ImagenesProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagenesProductos>> GetImagenesProductos(int id)
        {
          if (_context.ImagenesProductos == null)
          {
              return NotFound();
          }
            var imagenesProductos = await _context.ImagenesProductos.FindAsync(id);

            if (imagenesProductos == null)
            {
                return NotFound();
            }

            return imagenesProductos;
        }

        // PUT: api/ImagenesProductos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagenesProductos(int id, ImagenesProductos imagenesProductos)
        {
            if (id != imagenesProductos.IdImagen)
            {
                return BadRequest();
            }

            _context.Entry(imagenesProductos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagenesProductosExists(id))
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

        // POST: api/ImagenesProductos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImagenesProductos>> PostImagenesProductos(ImagenesProductos imagenesProductos)
        {
          if (_context.ImagenesProductos == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.ImagenesProductos'  is null.");
          }
            _context.ImagenesProductos.Add(imagenesProductos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagenesProductos", new { id = imagenesProductos.IdImagen }, imagenesProductos);
        }

        // DELETE: api/ImagenesProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagenesProductos(int id)
        {
            if (_context.ImagenesProductos == null)
            {
                return NotFound();
            }
            var imagenesProductos = await _context.ImagenesProductos.FindAsync(id);
            if (imagenesProductos == null)
            {
                return NotFound();
            }

            _context.ImagenesProductos.Remove(imagenesProductos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagenesProductosExists(int id)
        {
            return (_context.ImagenesProductos?.Any(e => e.IdImagen == id)).GetValueOrDefault();
        }
    }
}
