using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public CategoriasController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            try
            {
                var categorias = await _context.Categoria.ToListAsync();

                if (categorias == null || categorias.Count == 0)
                {
                    return NotFound("No se encontraron categorías.");
                }

                return categorias;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.IdCategoria == id);

                if (categoria == null)
                {
                    return NotFound("Categoría no encontrada.");
                }

                return categoria;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            if (_context.Categoria == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Categorias' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (TipoCategoria)
            if (_context.Categoria.Any(c => c.TipoCategoria == categoria.TipoCategoria))
            {
                ModelState.AddModelError("TipoCategoria", "El tipo de categoría ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Categoria.Add(categoria);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCategoria", new { id = categoria.IdCategoria }, categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest("El ID de la categoría no coincide con la categoría proporcionada.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (TipoCategoria)
            if (_context.Categoria.Any(c => c.TipoCategoria == categoria.TipoCategoria && c.IdCategoria != id))
            {
                ModelState.AddModelError("TipoCategoria", "El tipo de categoría ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Categoría actualizada exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound("Categoría no encontrada.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar la categoría.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categoria.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound("Categoría no encontrada.");
                }

                _context.Categoria.Remove(categoria);
                await _context.SaveChangesAsync();
                return Ok("Categoría borrada con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.IdCategoria == id);
        }
    }
}
