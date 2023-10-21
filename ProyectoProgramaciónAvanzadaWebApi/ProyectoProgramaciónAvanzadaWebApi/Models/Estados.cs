using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class Estados
    {
        [Key]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El campo Nombre del Estado es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Nombre del Estado debe tener como máximo 30 caracteres.")]
        [Display(Name = "Nombre del Estado")]
        public string NombreEstado { get; set; }
    }
}
