using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El campo Identificación es obligatorio.")]
        public string Identificacion { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        public string Contrasenna { get; set; }
    }
}
