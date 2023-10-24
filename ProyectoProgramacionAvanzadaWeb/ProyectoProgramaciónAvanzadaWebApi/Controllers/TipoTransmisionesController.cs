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
    public class TipoTransmisionesController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public TipoTransmisionesController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/TipoTransmisiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTransmisiones>>> GetTipoTransmisiones()
        {
            try
            {
                var tiposTransmision = await _context.TipoTransmisiones.ToListAsync();

                if (tiposTransmision == null || tiposTransmision.Count == 0)
                {
                    return NotFound("No se encontraron registros de tipos de transmisión.");
                }

                return tiposTransmision;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/TipoTransmisiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTransmisiones>> GetTipoTransmisiones(int id)
        {
            try
            {
                var tipoTransmision = await _context.TipoTransmisiones.FirstOrDefaultAsync(tt => tt.IdTransmision == id);

                if (tipoTransmision == null)
                {
                    return NotFound("Registro de tipo de transmisión no encontrado.");
                }

                return tipoTransmision;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/TipoTransmisiones
        [HttpPost]
        public async Task<ActionResult<TipoTransmisiones>> PostTipoTransmisiones(TipoTransmisiones tipoTransmision)
        {
            if (_context.TipoTransmisiones == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.TipoTransmisiones' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Transmisión)
            if (_context.TipoTransmisiones.Any(tt => tt.TipoTransmision == tipoTransmision.TipoTransmision))
            {
                ModelState.AddModelError("TipoTransmision", "El tipo de transmisión ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.TipoTransmisiones.Add(tipoTransmision);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTipoTransmisiones", new { id = tipoTransmision.IdTransmision }, tipoTransmision);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/TipoTransmisiones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoTransmisiones(int id, TipoTransmisiones tipoTransmision)
        {
            if (id != tipoTransmision.IdTransmision)
            {
                return BadRequest("El ID del registro de tipo de transmisión no coincide con el registro proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Transmisión)
            if (_context.TipoTransmisiones.Any(tt => tt.TipoTransmision == tipoTransmision.TipoTransmision && tt.IdTransmision != id))
            {
                ModelState.AddModelError("TipoTransmision", "El tipo de transmisión ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(tipoTransmision).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Registro de tipo de transmisión actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoTransmisionesExists(id))
                {
                    return NotFound("Registro de tipo de transmisión no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el registro de tipo de transmisión.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/TipoTransmisiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoTransmisiones(int id)
        {
            try
            {
                var tipoTransmision = await _context.TipoTransmisiones.FindAsync(id);
                if (tipoTransmision == null)
                {
                    return NotFound("Registro de tipo de transmisión no encontrado.");
                }

                _context.TipoTransmisiones.Remove(tipoTransmision);
                await _context.SaveChangesAsync();
                return Ok("Registro de tipo de transmisión borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool TipoTransmisionesExists(int id)
        {
            return _context.TipoTransmisiones.Any(tt => tt.IdTransmision == id);
        }
    }
}
