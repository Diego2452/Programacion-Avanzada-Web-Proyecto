using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public ProductosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
        {
            try
            {
                var productos = await _context.Productos.Include(p => p.Proveedor)
                                                       .Include(p => p.Categoria)
                                                       .Include(p => p.Imagenes)
                                                       .ToListAsync();

                if (productos == null || productos.Count == 0)
                {
                    return NotFound("No se encontraron productos.");
                }

                return productos;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProductos(int id)
        {
            try
            {
                var producto = await _context.Productos.Include(p => p.Proveedor)
                                                       .Include(p => p.Categoria)
                                                       .Include(p => p.Imagenes)
                                                       .FirstOrDefaultAsync(p => p.IdProducto == id);

                if (producto == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                return producto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Productos>> PostProducto(Productos producto)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Productos' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Nombre del Producto)
            if (_context.Productos.Any(p => p.NombreProducto == producto.NombreProducto))
            {
                ModelState.AddModelError("NombreProducto", "El nombre del producto ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProductos", new { id = producto.IdProducto }, producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Productos producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest("El ID del producto no coincide con el producto proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Nombre del Producto)
            if (_context.Productos.Any(p => p.NombreProducto == producto.NombreProducto && p.IdProducto != id))
            {
                ModelState.AddModelError("NombreProducto", "El nombre del producto ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Producto actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound("Producto no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el producto.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return Ok("Producto borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(p => p.IdProducto == id);
        }
    }
}
