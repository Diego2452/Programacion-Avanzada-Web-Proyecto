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
    public class ProveedoresController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public ProveedoresController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedores>>> GetProveedores()
        {
            try
            {
                var proveedores = await _context.Proveedores.ToListAsync();

                if (proveedores == null || proveedores.Count == 0)
                {
                    return NotFound("No se encontraron proveedores.");
                }

                return proveedores;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedores>> GetProveedor(int id)
        {
            try
            {
                var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.IdProveedor == id);

                if (proveedor == null)
                {
                    return NotFound("Proveedor no encontrado.");
                }

                return proveedor;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Proveedores
        [HttpPost]
        public async Task<ActionResult<Proveedores>> PostProveedor(Proveedores proveedor)
        {
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Proveedores' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Nombre del Proveedor)
            if (_context.Proveedores.Any(p => p.NombreProveedor == proveedor.NombreProveedor))
            {
                ModelState.AddModelError("NombreProveedor", "El nombre del proveedor ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Proveedores.Add(proveedor);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProveedor", new { id = proveedor.IdProveedor }, proveedor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedores proveedor)
        {
            if (id != proveedor.IdProveedor)
            {
                return BadRequest("El ID del proveedor no coincide con el proveedor proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Nombre del Proveedor)
            if (_context.Proveedores.Any(p => p.NombreProveedor == proveedor.NombreProveedor && p.IdProveedor != id))
            {
                ModelState.AddModelError("NombreProveedor", "El nombre del proveedor ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(proveedor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Proveedor actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
                {
                    return NotFound("Proveedor no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el proveedor.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            try
            {
                var proveedor = await _context.Proveedores.FindAsync(id);
                if (proveedor == null)
                {
                    return NotFound("Proveedor no encontrado.");
                }

                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();
                return Ok("Proveedor borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(p => p.IdProveedor == id);
        }
    }
}
