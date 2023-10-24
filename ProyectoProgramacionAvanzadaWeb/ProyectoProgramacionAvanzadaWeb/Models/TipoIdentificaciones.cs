using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class TipoIdentificaciones
    {
        [Key]
        public int IdIdentificacion { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Identificacion es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Tipo de Identificacion debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Identificacion")]
        public string TipoIdentificacion { get; set; }
    }
}
