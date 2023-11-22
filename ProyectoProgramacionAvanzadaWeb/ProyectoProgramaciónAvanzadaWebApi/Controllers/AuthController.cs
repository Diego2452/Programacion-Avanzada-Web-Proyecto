using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramaciónAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public AuthController(LuxuryCarsContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (user == null)
            {
                return Unauthorized(new { Errors = "El usuario no existe en la base de datos" });
            }

            if (user.Contrasenna != loginRequest.Contrasenna)
            {
                return Unauthorized(new { Errors = "Credenciales incorrectas" });
            }
            return Ok(new { Message = "Inicio de sesión exitoso" });
        }

    }
}
