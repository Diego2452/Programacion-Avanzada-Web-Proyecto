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
    public class SexosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public SexosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Sexo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sexo>>> GetSexo()
        {
            try
            {
                var sexos = await _context.Sexo.ToListAsync();

                if (sexos == null || sexos.Count == 0)
                {
                    return NotFound("No se encontraron registros de sexo.");
                }

                return sexos;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Sexo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sexo>> GetSexo(int id)
        {
            try
            {
                var sexo = await _context.Sexo.FirstOrDefaultAsync(s => s.IdSexo == id);

                if (sexo == null)
                {
                    return NotFound("Registro de sexo no encontrado.");
                }

                return sexo;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Sexo
        [HttpPost]
        public async Task<ActionResult<Sexo>> PostSexo(Sexo sexo)
        {
            if (_context.Sexo == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Sexo' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Sexo)
            if (_context.Sexo.Any(s => s.TipoSexo == sexo.TipoSexo))
            {
                ModelState.AddModelError("TipoSexo", "El tipo de sexo ya existe en la base de datos.");
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.Sexo.Add(sexo);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetSexo", new { id = sexo.IdSexo }, sexo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Sexo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSexo(int id, Sexo sexo)
        {
            if (id != sexo.IdSexo)
            {
                return BadRequest("El ID del registro de sexo no coincide con el registro proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Tipo de Sexo)
            if (_context.Sexo.Any(s => s.TipoSexo == sexo.TipoSexo && s.IdSexo != id))
            {
                ModelState.AddModelError("TipoSexo", "El tipo de sexo ya existe en la base de datos.");
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.Entry(sexo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Registro de sexo actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SexoExists(id))
                {
                    return NotFound("Registro de sexo no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el registro de sexo.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Sexo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSexo(int id)
        {
            try
            {
                var sexo = await _context.Sexo.FindAsync(id);
                if (sexo == null)
                {
                    return NotFound("Registro de sexo no encontrado.");
                }

                _context.Sexo.Remove(sexo);
                await _context.SaveChangesAsync();
                return Ok("Registro de sexo borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool SexoExists(int id)
        {
            return _context.Sexo.Any(s => s.IdSexo == id);
        }
    }
}
