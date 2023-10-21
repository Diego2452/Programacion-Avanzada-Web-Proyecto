using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class TipoCedulas
    {
        [Key]
        public int IdTipoCedula { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Cédula es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Tipo de Cédula debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Cédula")]
        public string TipoCedula { get; set; }
    }
}
