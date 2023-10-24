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
    public class TipoIdentificacionesController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public TipoIdentificacionesController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/TipoIdentificaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoIdentificaciones>>> GetTipoIdentificaciones()
        {
            try
            {
                var tiposIdentificacion = await _context.TipoIdentificaciones.ToListAsync();

                if (tiposIdentificacion == null || tiposIdentificacion.Count == 0)
                {
                    return NotFound("No se encontraron registros de tipos de identificación.");
                }

                return tiposIdentificacion;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/TipoIdentificaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoIdentificaciones>> GetTipoIdentificaciones(int id)
        {
            try
            {
                var tipoIdentificacion = await _context.TipoIdentificaciones.FirstOrDefaultAsync(ti => ti.IdIdentificacion == id);

                if (tipoIdentificacion == null)
                {
                    return NotFound("Registro de tipo de identificación no encontrado.");
                }

                return tipoIdentificacion;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/TipoIdentificaciones
        [HttpPost]
        public async Task<ActionResult<TipoIdentificaciones>> PostTipoIdentificaciones(TipoIdentificaciones tipoIdentificacion)
        {
            if (_context.TipoIdentificaciones == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.TipoIdentificaciones' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Identificación)
            if (_context.TipoIdentificaciones.Any(ti => ti.TipoIdentificacion == tipoIdentificacion.TipoIdentificacion))
            {
                ModelState.AddModelError("TipoIdentificacion", "El tipo de identificación ya existe en la base de datos.");
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.TipoIdentificaciones.Add(tipoIdentificacion);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTipoIdentificaciones", new { id = tipoIdentificacion.IdIdentificacion }, tipoIdentificacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/TipoIdentificaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoIdentificaciones(int id, TipoIdentificaciones tipoIdentificacion)
        {
            if (id != tipoIdentificacion.IdIdentificacion)
            {
                return BadRequest("El ID del registro de tipo de identificación no coincide con el registro proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Identificación)
            if (_context.TipoIdentificaciones.Any(ti => ti.TipoIdentificacion == tipoIdentificacion.TipoIdentificacion && ti.IdIdentificacion != id))
            {
                ModelState.AddModelError("TipoIdentificacion", "El tipo de identificación ya existe en la base de datos.");
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.Entry(tipoIdentificacion).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Registro de tipo de identificación actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoIdentificacionesExists(id))
                {
                    return NotFound("Registro de tipo de identificación no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el registro de tipo de identificación.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/TipoIdentificaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoIdentificaciones(int id)
        {
            try
            {
                var tipoIdentificacion = await _context.TipoIdentificaciones.FindAsync(id);
                if (tipoIdentificacion == null)
                {
                    return NotFound("Registro de tipo de identificación no encontrado.");
                }

                _context.TipoIdentificaciones.Remove(tipoIdentificacion);
                await _context.SaveChangesAsync();
                return Ok("Registro de tipo de identificación borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool TipoIdentificacionesExists(int id)
        {
            return _context.TipoIdentificaciones.Any(ti => ti.IdIdentificacion == id);
        }
    }
}
