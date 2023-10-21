using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class Roles
    {
        [Key]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El campo Nombre de Rol es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Nombre de Rol debe tener como máximo 30 caracteres.")]
        [Display(Name = "Nombre de Rol")]
        public string NombreRol { get; set; }
    }
}
