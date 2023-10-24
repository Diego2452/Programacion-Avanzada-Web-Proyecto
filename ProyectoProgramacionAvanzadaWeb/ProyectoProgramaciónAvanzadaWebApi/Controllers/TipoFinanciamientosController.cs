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
    public class TipoFinanciamientosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public TipoFinanciamientosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/TipoFinanciamientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoFinanciamientos>>> GetTipoFinanciamientos()
        {
            try
            {
                var tiposFinanciamiento = await _context.TipoFinanciamientos.ToListAsync();

                if (tiposFinanciamiento == null || tiposFinanciamiento.Count == 0)
                {
                    return NotFound("No se encontraron registros de tipos de financiamiento.");
                }

                return tiposFinanciamiento;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/TipoFinanciamientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoFinanciamientos>> GetTipoFinanciamientos(int id)
        {
            try
            {
                var tipoFinanciamiento = await _context.TipoFinanciamientos.FirstOrDefaultAsync(tf => tf.IdFinanciamiento == id);

                if (tipoFinanciamiento == null)
                {
                    return NotFound("Registro de tipo de financiamiento no encontrado.");
                }

                return tipoFinanciamiento;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/TipoFinanciamientos
        [HttpPost]
        public async Task<ActionResult<TipoFinanciamientos>> PostTipoFinanciamientos(TipoFinanciamientos tipoFinanciamiento)
        {
            if (_context.TipoFinanciamientos == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.TipoFinanciamientos' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Financiamiento)
            if (_context.TipoFinanciamientos.Any(tf => tf.TipoFinanciamiento == tipoFinanciamiento.TipoFinanciamiento))
            {
                ModelState.AddModelError("TipoFinanciamiento", "El tipo de financiamiento ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.TipoFinanciamientos.Add(tipoFinanciamiento);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTipoFinanciamientos", new { id = tipoFinanciamiento.IdFinanciamiento }, tipoFinanciamiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/TipoFinanciamientos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoFinanciamientos(int id, TipoFinanciamientos tipoFinanciamiento)
        {
            if (id != tipoFinanciamiento.IdFinanciamiento)
            {
                return BadRequest("El ID del registro de tipo de financiamiento no coincide con el registro proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Financiamiento)
            if (_context.TipoFinanciamientos.Any(tf => tf.TipoFinanciamiento == tipoFinanciamiento.TipoFinanciamiento && tf.IdFinanciamiento != id))
            {
                ModelState.AddModelError("TipoFinanciamiento", "El tipo de financiamiento ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(tipoFinanciamiento).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Registro de tipo de financiamiento actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoFinanciamientosExists(id))
                {
                    return NotFound("Registro de tipo de financiamiento no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el registro de tipo de financiamiento.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/TipoFinanciamientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoFinanciamientos(int id)
        {
            try
            {
                var tipoFinanciamiento = await _context.TipoFinanciamientos.FindAsync(id);
                if (tipoFinanciamiento == null)
                {
                    return NotFound("Registro de tipo de financiamiento no encontrado.");
                }

                _context.TipoFinanciamientos.Remove(tipoFinanciamiento);
                await _context.SaveChangesAsync();
                return Ok("Registro de tipo de financiamiento borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool TipoFinanciamientosExists(int id)
        {
            return _context.TipoFinanciamientos.Any(tf => tf.IdFinanciamiento == id);
        }
    }
}