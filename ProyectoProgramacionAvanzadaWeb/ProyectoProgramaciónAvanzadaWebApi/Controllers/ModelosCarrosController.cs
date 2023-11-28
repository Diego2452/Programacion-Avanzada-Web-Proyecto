using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelosCarrosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public ModelosCarrosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/ModelosCarros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelosCarros>>> GetModelosCarros()
        {
            try
            {
                var modelosCarros = await _context.ModelosCarros.Include(mc => mc.Marca).ToListAsync();

                if (modelosCarros == null || modelosCarros.Count == 0)
                {
                    return NotFound("No se encontraron modelos de carros.");
                }

                return modelosCarros;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/ModelosCarros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelosCarros>> GetModelosCarros(int id)
        {
            try
            {
                var modelosCarros = await _context.ModelosCarros.Include(mc => mc.Marca).FirstOrDefaultAsync(mc => mc.IdModelo == id);

                if (modelosCarros == null)
                {
                    return NotFound("Modelo de carro no encontrado.");
                }

                return modelosCarros;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/ModelosCarros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelosCarros(int id, ModelosCarros modelosCarros)
        {
            if (id != modelosCarros.IdModelo)
            {
                return BadRequest("El ID del modelo no coincide con el modelo proporcionado.");
            }

            var existingModelo = _context.ModelosCarros.FirstOrDefault(mc => mc.Modelo == modelosCarros.Modelo && mc.IdModelo != modelosCarros.IdModelo);

            if (existingModelo != null)
            {
                return BadRequest("Este modelo ya existe en la base de datos.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(modelosCarros).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Modelo de carro actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelosCarrosExists(id))
                {
                    return NotFound("Modelo de carro no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el modelo de carro.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/ModelosCarros
        [HttpPost]
        public async Task<ActionResult<ModelosCarros>> PostModelosCarros(ModelosCarros modelosCarros)
        {
            if (_context.ModelosCarros == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.ModelosCarros' is null.");
            }

            var existingModelo = _context.ModelosCarros.FirstOrDefault(mc => mc.Modelo == modelosCarros.Modelo);

            if (existingModelo != null)
            {
                return BadRequest("Este modelo ya existe en la base de datos.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.ModelosCarros.Add(modelosCarros);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetModelosCarros", new { id = modelosCarros.IdModelo }, modelosCarros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/ModelosCarros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelosCarros(int id)
        {
            try
            {
                var modelosCarros = await _context.ModelosCarros.FindAsync(id);
                if (modelosCarros == null)
                {
                    return NotFound("Modelo de carro no encontrado.");
                }

                _context.ModelosCarros.Remove(modelosCarros);
                await _context.SaveChangesAsync();
                return Ok("Modelo de carro borrado con éxito.");
                //return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool ModelosCarrosExists(int id)
        {
            return _context.ModelosCarros.Any(e => e.IdModelo == id);
        }
    }

}
