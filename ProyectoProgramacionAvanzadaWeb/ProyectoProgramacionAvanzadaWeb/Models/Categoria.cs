using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Categoría es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Tipo de Categoría debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Categoría")]
        public string TipoCategoria { get; set; }
    }
}
