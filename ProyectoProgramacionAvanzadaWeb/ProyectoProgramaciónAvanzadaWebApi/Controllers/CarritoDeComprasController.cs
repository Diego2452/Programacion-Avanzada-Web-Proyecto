using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoDeComprasController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public CarritoDeComprasController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/CarritoDeCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarritoDeCompras>>> GetCarritoDeCompras()
        {
            try
            {
                var carritos = await _context.CarritoDeCompras.Include(c => c.Estado)
                                                              .Include(c => c.Producto)
                                                              .Include(c => c.Usuario)
                                                              .ToListAsync();

                if (carritos == null || carritos.Count == 0)
                {
                    return NotFound("No se encontraron carritos de compras.");
                }

                return carritos;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/CarritoDeCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDeCompras>> GetCarritoDeCompras(int id)
        {
            try
            {
                var carrito = await _context.CarritoDeCompras.Include(c => c.Estado)
                                                            .Include(c => c.Producto)
                                                            .Include(c => c.Usuario)
                                                            .FirstOrDefaultAsync(c => c.IdCarrito == id);

                if (carrito == null)
                {
                    return NotFound("Carrito de compras no encontrado.");
                }

                return carrito;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/CarritoDeCompras
        [HttpPost]
        public async Task<ActionResult<CarritoDeCompras>> PostCarritoDeCompras(CarritoDeCompras carrito)
        {
            if (_context.CarritoDeCompras == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.CarritoDeCompras' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.CarritoDeCompras.Add(carrito);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCarritoDeCompras", new { id = carrito.IdCarrito }, carrito);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/CarritoDeCompras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarritoDeCompras(int id, CarritoDeCompras carrito)
        {
            if (id != carrito.IdCarrito)
            {
                return BadRequest("El ID del carrito de compras no coincide con el carrito proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(carrito).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Carrito de compras actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoDeComprasExists(id))
                {
                    return NotFound("Carrito de compras no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el carrito de compras.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/CarritoDeCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarritoDeCompras(int id)
        {
            try
            {
                var carrito = await _context.CarritoDeCompras.FindAsync(id);
                if (carrito == null)
                {
                    return NotFound("Carrito de compras no encontrado.");
                }

                _context.CarritoDeCompras.Remove(carrito);
                await _context.SaveChangesAsync();
                return Ok("Carrito de compras borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool CarritoDeComprasExists(int id)
        {
            return _context.CarritoDeCompras.Any(e => e.IdCarrito == id);
        }
    }
}
