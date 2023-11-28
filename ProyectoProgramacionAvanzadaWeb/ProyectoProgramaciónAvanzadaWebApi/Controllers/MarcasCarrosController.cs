using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasCarrosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public MarcasCarrosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/MarcasCarros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcasCarros>>> GetMarcasCarros()
        {
            try
            {
                var marcas = await _context.MarcasCarros.ToListAsync();

                if (marcas == null || marcas.Count == 0)
                {
                    return NotFound("No se encontraron marcas de carros.");
                }

                return marcas;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/MarcasCarros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarcasCarros>> GetMarcasCarros(int id)
        {
            try
            {
                var marca = await _context.MarcasCarros.FirstOrDefaultAsync(m => m.IdMarca == id);

                if (marca == null)
                {
                    return NotFound("Marca de carro no encontrada.");
                }

                return marca;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/MarcasCarros
        [HttpPost]
        public async Task<ActionResult<MarcasCarros>> PostMarcasCarros(MarcasCarros marca)
        {
            if (_context.MarcasCarros == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.MarcasCarros' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Marca)
            if (_context.MarcasCarros.Any(m => m.Marca == marca.Marca))
            {
                ModelState.AddModelError("Marca", "La marca de carro ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.MarcasCarros.Add(marca);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetMarcasCarros", new { id = marca.IdMarca }, marca);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/MarcasCarros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarcasCarros(int id, MarcasCarros marca)
        {
            if (id != marca.IdMarca)
            {
                return BadRequest("El ID de la marca de carro no coincide con la marca proporcionada.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Marca)
            if (_context.MarcasCarros.Any(m => m.Marca == marca.Marca && m.IdMarca != id))
            {
                ModelState.AddModelError("Marca", "La marca de carro ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(marca).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Marca de carro actualizada exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcasCarrosExists(id))
                {
                    return NotFound("Marca de carro no encontrada.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar la marca de carro.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/MarcasCarros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarcasCarros(int id)
        {
            try
            {
                var marca = await _context.MarcasCarros.FindAsync(id);
                if (marca == null)
                {
                    return NotFound("Marca de carro no encontrada.");
                }

                _context.MarcasCarros.Remove(marca);
                await _context.SaveChangesAsync();
                return Ok("Marca de carro borrada con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool MarcasCarrosExists(int id)
        {
            return _context.MarcasCarros.Any(m => m.IdMarca == id);
        }
    }
}
