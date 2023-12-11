using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class LoginRequest
    {
        [Key]
        public int? id { get; set; }
        [Required(ErrorMessage = "El campo Identificación es obligatorio.")]
        public string Identificacion { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        public string Contrasenna { get; set; }
    }
}
