using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class LoginRequest
    {
        [Key]
        public int? id { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo Email debe ser una dirección de correo electrónico válida.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        public string Contrasenna { get; set; }
    }
}
