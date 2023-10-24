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
    public class TipoCombustiblesController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public TipoCombustiblesController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/TipoCombustibles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCombustibles>>> GetTipoCombustibles()
        {
            try
            {
                var tiposCombustible = await _context.TipoCombustibles.ToListAsync();

                if (tiposCombustible == null || tiposCombustible.Count == 0)
                {
                    return NotFound("No se encontraron registros de tipos de combustible.");
                }

                return tiposCombustible;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/TipoCombustibles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCombustibles>> GetTipoCombustibles(int id)
        {
            try
            {
                var tipoCombustible = await _context.TipoCombustibles.FirstOrDefaultAsync(tc => tc.IdCombustible == id);

                if (tipoCombustible == null)
                {
                    return NotFound("Registro de tipo de combustible no encontrado.");
                }

                return tipoCombustible;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/TipoCombustibles
        [HttpPost]
        public async Task<ActionResult<TipoCombustibles>> PostTipoCombustibles(TipoCombustibles tipoCombustible)
        {
            if (_context.TipoCombustibles == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.TipoCombustibles' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Combustible)
            if (_context.TipoCombustibles.Any(tc => tc.TipoCombustible == tipoCombustible.TipoCombustible))
            {
                ModelState.AddModelError("TipoCombustible", "El tipo de combustible ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.TipoCombustibles.Add(tipoCombustible);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTipoCombustibles", new { id = tipoCombustible.IdCombustible }, tipoCombustible);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/TipoCombustibles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoCombustibles(int id, TipoCombustibles tipoCombustible)
        {
            if (id != tipoCombustible.IdCombustible)
            {
                return BadRequest("El ID del registro de tipo de combustible no coincide con el registro proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Combustible)
            if (_context.TipoCombustibles.Any(tc => tc.TipoCombustible == tipoCombustible.TipoCombustible && tc.IdCombustible != id))
            {
                ModelState.AddModelError("TipoCombustible", "El tipo de combustible ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(tipoCombustible).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Registro de tipo de combustible actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCombustiblesExists(id))
                {
                    return NotFound("Registro de tipo de combustible no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el registro de tipo de combustible.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/TipoCombustibles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoCombustibles(int id)
        {
            try
            {
                var tipoCombustible = await _context.TipoCombustibles.FindAsync(id);
                if (tipoCombustible == null)
                {
                    return NotFound("Registro de tipo de combustible no encontrado.");
                }

                _context.TipoCombustibles.Remove(tipoCombustible);
                await _context.SaveChangesAsync();
                return Ok("Registro de tipo de combustible borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool TipoCombustiblesExists(int id)
        {
            return _context.TipoCombustibles.Any(tc => tc.IdCombustible == id);
        }
    }
}
