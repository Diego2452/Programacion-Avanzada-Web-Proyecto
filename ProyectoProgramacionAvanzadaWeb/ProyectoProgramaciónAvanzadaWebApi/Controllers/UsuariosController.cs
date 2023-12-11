using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public UsuariosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios.Include(u => u.Genero)
                                                      .Include(u => u.TipoIdentificacion)
                                                      .Include(u => u.Rol)
                                                      .ToListAsync();

                if (usuarios == null || usuarios.Count == 0)
                {
                    return NotFound("No se encontraron usuarios.");
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.Include(u => u.Genero)
                                                    .Include(u => u.TipoIdentificacion)
                                                    .Include(u => u.Rol)
                                                    .FirstOrDefaultAsync(u => u.IdUsuario == id);

                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                return usuario;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuario(Usuarios usuario)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'LuxuryCarsContext.Usuarios' is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar campos únicos (Identificacion, Email, Telefono) por separado
            if (_context.Usuarios.Any(u => u.Identificacion == usuario.Identificacion))
            {
                ModelState.AddModelError("Identificacion", "El número de identificación ya existe en la base de datos.");
            }

            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            {
                ModelState.AddModelError("Email", "La dirección de correo electrónico ya existe en la base de datos.");
            }

            if (_context.Usuarios.Any(u => u.Telefono == usuario.Telefono))
            {
                ModelState.AddModelError("Telefono", "El número de teléfono ya existe en la base de datos.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUsuarios", new { id = usuario.IdUsuario }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuarios usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest("El ID del usuario no coincide con el usuario proporcionado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Usuarios.Any(u => u.Identificacion == usuario.Identificacion && u.IdUsuario != id))
            {
                ModelState.AddModelError("Identificacion", "El número de identificación ya existe en la base de datos.");
            }

            if (_context.Usuarios.Any(u => u.Email == usuario.Email && u.IdUsuario != id))
            {
                ModelState.AddModelError("Email", "La dirección de correo electrónico ya existe en la base de datos.");
            }

            if (_context.Usuarios.Any(u => u.Telefono == usuario.Telefono && u.IdUsuario != id))
            {
                ModelState.AddModelError("Telefono", "El número de teléfono ya existe en la base de datos.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = ModelState });
            }

            try
            {
                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Usuario actualizado exitosamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound("Usuario no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error interno del servidor al actualizar el usuario.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return Ok("Usuario borrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}