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
    public class MetodosDePagoController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public MetodosDePagoController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/MetodosDePago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodosDePago>>> GetMetodosDePago()
        {
            try
            {
                var metodosDePago = await _context.MetodosDePago.ToListAsync();

                if (metodosDePago == null || metodosDePago.Count == 0)
                {
                    return NotFound("No se encontraron métodos de pago.");
                }

                return metodosDePago;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/MetodosDePago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetodosDePago>> GetMetodosDePago(int id)
        {
            try
            {
                var metodoPago = await _context.MetodosDePago.FirstOrDefaultAsync(m => m.IdMetodoPago == id);

                if (metodoPago == null)
                {
                    return NotFound("Método de pago no encontrado.");
                }

                return metodoPago;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/MetodosDePago
        [HttpPost]
        public async Task<ActionResult<MetodosDePago>> PostMetodosDePago(MetodosDePago metodoPago)
        {
            if (_context.MetodosDePago == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.MetodosDePago' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Nombre del Método de Pago)
            if (_context.MetodosDePago.Any(m => m.NombreMetodo == metodoPago.NombreMetodo))
            {
                ModelState.AddModelError("NombreMetodo", "El nombre del método de pago ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.MetodosDePago.Add(metodoPago);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetMetodosDePago", new { id = metodoPago.IdMetodoPago }, metodoPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/MetodosDePago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodosDePago(int id, MetodosDePago metodoPago)
        {
            if (id != metodoPago.IdMetodoPago)
            {
                return BadRequest("El ID del método de pago no coincide con el método de pago proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Nombre del Método de Pago)
            if (_context.MetodosDePago.Any(m => m.NombreMetodo == metodoPago.NombreMetodo && m.IdMetodoPago != id))
            {
                ModelState.AddModelError("NombreMetodo", "El nombre del método de pago ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(metodoPago).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Método de pago actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodosDePagoExists(id))
                {
                    return NotFound("Método de pago no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el método de pago.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/MetodosDePago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodosDePago(int id)
        {
            try
            {
                var metodoPago = await _context.MetodosDePago.FindAsync(id);
                if (metodoPago == null)
                {
                    return NotFound("Método de pago no encontrado.");
                }

                _context.MetodosDePago.Remove(metodoPago);
                await _context.SaveChangesAsync();
                return Ok("Método de pago borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool MetodosDePagoExists(int id)
        {
            return _context.MetodosDePago.Any(m => m.IdMetodoPago == id);
        }
    }
}
