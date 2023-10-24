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
    public class FacturacionesController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public FacturacionesController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Facturacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facturacion>>> GetFacturacion()
        {
            try
            {
                var facturas = await _context.Facturacion
                    .Include(f => f.Estado)
                    .Include(f => f.Usuario)
                    .Include(f => f.MetodoPago)
                    .ToListAsync();

                if (facturas == null || facturas.Count == 0)
                {
                    return NotFound("No se encontraron facturas.");
                }

                return facturas;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Facturacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facturacion>> GetFactura(int id)
        {
            try
            {
                var factura = await _context.Facturacion
                    .Include(f => f.Estado)
                    .Include(f => f.Usuario)
                    .Include(f => f.MetodoPago)
                    .FirstOrDefaultAsync(f => f.IdFactura == id);

                if (factura == null)
                {
                    return NotFound("Factura no encontrada.");
                }

                return factura;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Facturacion
        [HttpPost]
        public async Task<ActionResult<Facturacion>> PostFactura(Facturacion factura)
        {
            if (_context.Facturacion == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Facturacion' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (NumeroFactura)
            if (_context.Facturacion.Any(f => f.NumeroFactura == factura.NumeroFactura))
            {
                ModelState.AddModelError("NumeroFactura", "El número de factura ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Facturacion.Add(factura);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetFactura", new { id = factura.IdFactura }, factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Facturacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Facturacion factura)
        {
            if (id != factura.IdFactura)
            {
                return BadRequest("El ID de la factura no coincide con la factura proporcionada.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (NumeroFactura)
            if (_context.Facturacion.Any(f => f.NumeroFactura == factura.NumeroFactura && f.IdFactura != id))
            {
                ModelState.AddModelError("NumeroFactura", "El número de factura ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(factura).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Factura actualizada exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
                {
                    return NotFound("Factura no encontrada.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar la factura.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Facturacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            try
            {
                var factura = await _context.Facturacion.FindAsync(id);
                if (factura == null)
                {
                    return NotFound("Factura no encontrada.");
                }

                _context.Facturacion.Remove(factura);
                await _context.SaveChangesAsync();
                return Ok("Factura borrada con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturacion.Any(f => f.IdFactura == id);
        }
    }
}
