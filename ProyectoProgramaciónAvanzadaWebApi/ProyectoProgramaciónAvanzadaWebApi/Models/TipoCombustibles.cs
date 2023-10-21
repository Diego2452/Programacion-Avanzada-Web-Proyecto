using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class TipoCombustibles
    {
        [Key]
        public int IdCombustible { get; set; }

        [Required(ErrorMessage = "El campo TipoCombustible es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo TipoCombustible debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Combustible")]
        public string TipoCombustible { get; set; }
    }
}
