using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class TipoIdentificaciones
    {
        [Key]
        public int IdIdentificacion { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Cédula es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Tipo de Cédula debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Identificacion")]
        public string TipoIdentificacion { get; set; } //Valor Unico
    }
}
