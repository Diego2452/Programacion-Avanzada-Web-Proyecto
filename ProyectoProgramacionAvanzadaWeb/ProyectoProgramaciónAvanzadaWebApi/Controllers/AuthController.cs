using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;
using ProyectoProgramaciónAvanzadaWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoProgramaciónAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(LuxuryCarsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                loginRequest.Identificacion = "402560159";
                loginRequest.Contrasenna = "1234";
                var usuario = await _context.Usuarios
                    .Include(u => u.Rol) 
                    .FirstOrDefaultAsync(u =>(u.Identificacion == loginRequest.Identificacion || u.Email == loginRequest.Identificacion));

                if (usuario == null)
                {
                    return Unauthorized(new { message = "Credenciales inválidas" });
                }

                var pass = BCrypt.Net.BCrypt.Verify(loginRequest.Contrasenna, usuario.Contrasenna);

                if (!BCrypt.Net.BCrypt.Verify(loginRequest.Contrasenna, usuario.Contrasenna))
                {
                    return Unauthorized(new { message = "Credenciales inválidas"});
                }

                var rol = usuario.Rol?.NombreRol;

                var token = GenerateJwtToken(usuario, rol);

                var tokenData = new Token
                {
                    access_token = token,
                    token_type = "Bearer",
                    expires_in = 3600,
                    exp = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds(),
                    refresh_token = ""
                };

                var userData = new Usuarios
                {
                    IdUsuario = usuario.IdUsuario,
                    IdTipoIdentificacion = usuario.IdTipoIdentificacion,
                    IdRol = usuario.IdRol,
                    Identificacion = usuario.Identificacion,
                    Nombre = usuario.Nombre,
                    Apellido_Materno = usuario.Apellido_Materno,
                    Apellido_Paterno = usuario.Apellido_Paterno,
                    Email = usuario.Email,
                    IdGenero = usuario.IdGenero,
                    Telefono = usuario.Telefono,
                    Direccion = usuario.Direccion
                };

                return Ok(new { tokenData = tokenData, userData = userData});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor: " + ex.Message });
            }
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var tokenValidationResult = Token.ValidarToken(identity);

                if (!tokenValidationResult.Success)
                {
                    if (tokenValidationResult.Message == "El token ha expirado")
                    {
                        var usuarioIdClaim = identity.FindFirst("Id");
                        if (usuarioIdClaim == null)
                        {
                            return Unauthorized(new { message = "No se puede encontrar la información del usuario en el token." });
                        }

                        var usuarioId = int.Parse(usuarioIdClaim.Value);
                        var usuario = _context.Usuarios.Include(u => u.Rol).FirstOrDefault(u => u.IdUsuario == usuarioId);

                        if (usuario == null)
                        {
                            return Unauthorized(new { message = "No se puede encontrar el usuario correspondiente al token expirado." });
                        }
                        var rol = usuario.Rol?.NombreRol;
                        var newToken = GenerateJwtToken(usuario, rol);

                        return Ok(new { message = "Token refrescado", newToken = newToken });
                    }
                    else
                    {
                        return Unauthorized(new { message = "Token inválido: " + tokenValidationResult.Message });
                    }
                }

                return Ok(new { message = "Token válido" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor: " + ex.Message });
            }
        }

        private string GenerateJwtToken(Usuarios usuario, string rol)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Id", usuario.IdUsuario.ToString()),
            new Claim("Rol", rol),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
