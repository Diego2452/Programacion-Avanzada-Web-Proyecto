using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialCompraCarrosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public HistorialCompraCarrosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/HistorialCompraCarro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialCompraCarro>>> GetHistorialCompraCarro()
        {
            try
            {
                var historiales = await _context.HistorialCompraCarro
                    .Include(h => h.Usuario)
                    .Include(h => h.Carrito)
                    .Include(h => h.Estado)
                    .ToListAsync();

                if (historiales == null || historiales.Count == 0)
                {
                    return NotFound("No se encontraron historiales de compra de carros.");
                }

                return historiales;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/HistorialCompraCarro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialCompraCarro>> GetHistorialCompraCarro(int id)
        {
            try
            {
                var historial = await _context.HistorialCompraCarro
                    .Include(h => h.Usuario)
                    .Include(h => h.Carrito)
                    .Include(h => h.Estado)
                    .FirstOrDefaultAsync(h => h.IdHistorialCompraCarro == id);

                if (historial == null)
                {
                    return NotFound("Historial de compra de carro no encontrado.");
                }

                return historial;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/HistorialCompraCarro
        [HttpPost]
        public async Task<ActionResult<HistorialCompraCarro>> PostHistorialCompraCarro(HistorialCompraCarro historial)
        {
            if (_context.HistorialCompraCarro == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.HistorialCompraCarro' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.HistorialCompraCarro.Add(historial);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetHistorialCompraCarro", new { id = historial.IdHistorialCompraCarro }, historial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/HistorialCompraCarro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialCompraCarro(int id, HistorialCompraCarro historial)
        {
            if (id != historial.IdHistorialCompraCarro)
            {
                return BadRequest("El ID de historial de compra de carro no coincide con el historial proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(historial).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Historial de compra de carro actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialCompraCarroExists(id))
                {
                    return NotFound("Historial de compra de carro no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el historial de compra de carro.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/HistorialCompraCarro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialCompraCarro(int id)
        {
            try
            {
                var historial = await _context.HistorialCompraCarro.FindAsync(id);
                if (historial == null)
                {
                    return NotFound("Historial de compra de carro no encontrado.");
                }

                _context.HistorialCompraCarro.Remove(historial);
                await _context.SaveChangesAsync();
                return Ok("Historial de compra de carro borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool HistorialCompraCarroExists(int id)
        {
            return _context.HistorialCompraCarro.Any(h => h.IdHistorialCompraCarro == id);
        }
    }
}
