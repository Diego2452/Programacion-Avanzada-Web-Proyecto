using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class Sexo
    {
        [Key]
        public int IdSexo { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Genero es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Tipo de Genero debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Genero")]
        public string TipoSexo { get; set; }
    }
}
