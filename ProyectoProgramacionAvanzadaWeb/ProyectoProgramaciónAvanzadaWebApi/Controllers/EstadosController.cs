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
    public class EstadosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public EstadosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Estados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estados>>> GetEstados()
        {
            try
            {
                var estados = await _context.Estados.ToListAsync();

                if (estados == null || estados.Count == 0)
                {
                    return NotFound("No se encontraron estados.");
                }

                return estados;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Estados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estados>> GetEstado(int id)
        {
            try
            {
                var estado = await _context.Estados.FirstOrDefaultAsync(e => e.IdEstado == id);

                if (estado == null)
                {
                    return NotFound("Estado no encontrado.");
                }

                return estado;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Estados
        [HttpPost]
        public async Task<ActionResult<Estados>> PostEstado(Estados estado)
        {
            if (_context.Estados == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Estados' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (NombreEstado)
            if (_context.Estados.Any(e => e.NombreEstado == estado.NombreEstado))
            {
                ModelState.AddModelError("NombreEstado", "El nombre del estado ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Estados.Add(estado);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetEstado", new { id = estado.IdEstado }, estado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Estados/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estados estado)
        {
            if (id != estado.IdEstado)
            {
                return BadRequest("El ID del estado no coincide con el estado proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (NombreEstado)
            if (_context.Estados.Any(e => e.NombreEstado == estado.NombreEstado && e.IdEstado != id))
            {
                ModelState.AddModelError("NombreEstado", "El nombre del estado ya existe en la base de datos.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(estado).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Estado actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
                {
                    return NotFound("Estado no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el estado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Estados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            try
            {
                var estado = await _context.Estados.FindAsync(id);
                if (estado == null)
                {
                    return NotFound("Estado no encontrado.");
                }

                _context.Estados.Remove(estado);
                await _context.SaveChangesAsync();
                return Ok("Estado borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.IdEstado == id);
        }
    }
}
