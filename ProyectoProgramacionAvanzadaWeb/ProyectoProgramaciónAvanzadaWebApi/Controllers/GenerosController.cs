using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public GenerosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Genero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGenero()
        {
            try
            {
                var generos = await _context.Genero.ToListAsync();

                if (generos == null || generos.Count == 0)
                {
                    return NotFound("No se encontraron registros de Genero.");
                }

                return generos;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Genero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            try
            {
                var Genero = await _context.Genero.FirstOrDefaultAsync(s => s.IdGenero == id);

                if (Genero == null)
                {
                    return NotFound("Registro de Genero no encontrado.");
                }

                return Genero;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Genero
        [HttpPost]
        public async Task<ActionResult<Genero>> PostGenero(Genero Genero)
        {
            if (_context.Genero == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Genero' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Genero)
            if (_context.Genero.Any(s => s.TipoGenero == Genero.TipoGenero))
            {
                ModelState.AddModelError("TipoGenero", "El tipo de Genero ya existe en la base de datos.");
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.Genero.Add(Genero);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetGenero", new { id = Genero.IdGenero }, Genero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Genero/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, Genero Genero)
        {
            if (id != Genero.IdGenero)
            {
                return BadRequest("El ID del registro de Genero no coincide con el registro proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Genero)
            if (_context.Genero.Any(s => s.TipoGenero == Genero.TipoGenero && s.IdGenero != id))
            {
                ModelState.AddModelError("TipoGenero", "El tipo de Genero ya existe en la base de datos.");
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.Entry(Genero).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Registro de Genero actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
                {
                    return NotFound("Registro de Genero no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el registro de Genero.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Genero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            try
            {
                var Genero = await _context.Genero.FindAsync(id);
                if (Genero == null)
                {
                    return NotFound("Registro de Genero no encontrado.");
                }

                _context.Genero.Remove(Genero);
                await _context.SaveChangesAsync();
                return Ok("Registro de Genero borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool GeneroExists(int id)
        {
            return _context.Genero.Any(s => s.IdGenero == id);
        }
    }
}
