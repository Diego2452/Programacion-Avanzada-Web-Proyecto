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
    public class RolesController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public RolesController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
            try
            {
                var roles = await _context.Roles.ToListAsync();

                if (roles == null || roles.Count == 0)
                {
                    return NotFound("No se encontraron roles.");
                }

                return roles;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRol(int id)
        {
            try
            {
                var rol = await _context.Roles.FirstOrDefaultAsync(r => r.IdRol == id);

                if (rol == null)
                {
                    return NotFound("Rol no encontrado.");
                }

                return rol;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Roles>> PostRol(Roles rol)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Roles' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Nombre de Rol)
            if (_context.Roles.Any(r => r.NombreRol == rol.NombreRol))
            {
                ModelState.AddModelError("NombreRol", "El nombre del rol ya existe en la base de datos.");
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.Roles.Add(rol);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetRol", new { id = rol.IdRol }, rol);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Roles rol)
        {
            if (id != rol.IdRol)
            {
                return BadRequest("El ID del rol no coincide con el rol proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campo único (Nombre de Rol)
            if (_context.Roles.Any(r => r.NombreRol == rol.NombreRol && r.IdRol != id))
            {
                ModelState.AddModelError("NombreRol", "El nombre del rol ya existe en la base de datos.");
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.Entry(rol).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Rol actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
                {
                    return NotFound("Rol no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el rol.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);
                if (rol == null)
                {
                    return NotFound("Rol no encontrado.");
                }

                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
                return Ok("Rol borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool RolExists(int id)
        {
            return _context.Roles.Any(r => r.IdRol == id);
        }
    }
}
