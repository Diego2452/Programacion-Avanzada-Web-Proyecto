using System;
using System.Linq;
using System.Security.Claims;

namespace ProyectoProgramaciónAvanzadaWebApi.Models
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int? expires_in { get; set; }
        public long? exp { get; set; }
        public string refresh_token { get; set; }

        public class TokenValidationResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string Result { get; set; }
        }

        public static TokenValidationResult ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new TokenValidationResult
                    {
                        Success = false,
                        Message = "Verificar si estás enviando un token válido",
                        Result = ""
                    };
                }

                var expClaim = identity.Claims.FirstOrDefault(x => x.Type == "exp");

                if (expClaim == null || !long.TryParse(expClaim.Value, out var expirationTime))
                {
                    return new TokenValidationResult
                    {
                        Success = false,
                        Message = "El token no contiene información de expiración válida",
                        Result = ""
                    };
                }

                var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                if (currentTime >= expirationTime)
                {
                    return new TokenValidationResult
                    {
                        Success = false,
                        Message = "El token ha expirado",
                        Result = ""
                    };
                }

                return new TokenValidationResult
                {
                    Success = true,
                    Message = "Éxito",
                    Result = "Token válido"
                };
            }
            catch (Exception ex)
            {
                return new TokenValidationResult
                {
                    Success = false,
                    Message = "Error en la validación del token: " + ex.Message,
                    Result = ""
                };
            }
        }
    }
}
