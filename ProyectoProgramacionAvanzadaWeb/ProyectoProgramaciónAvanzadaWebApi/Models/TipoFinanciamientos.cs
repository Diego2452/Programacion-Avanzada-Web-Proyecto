using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class TipoFinanciamientos
    {
        [Key]
        public int IdFinanciamiento { get; set; }

        [Required(ErrorMessage = "El campo TipoFinanciamiento es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo TipoFinanciamiento debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Financiamiento")]
        public string TipoFinanciamiento { get; set; } //Valor Unico
    }
}
