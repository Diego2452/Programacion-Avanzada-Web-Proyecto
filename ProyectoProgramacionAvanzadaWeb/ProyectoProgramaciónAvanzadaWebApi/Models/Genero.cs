using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class Genero
    {
        [Key]
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Genero es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Tipo de Genero debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Genero")]
        public string TipoGenero { get; set; } //Valor unico
    }
}
