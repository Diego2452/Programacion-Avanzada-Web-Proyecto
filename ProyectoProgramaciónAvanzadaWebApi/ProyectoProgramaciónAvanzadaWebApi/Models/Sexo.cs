using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class Sexo
    {
        [Key]
        public int IdSexo { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Sexo es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Tipo de Sexo debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Sexo")]
        public string TipoSexo { get; set; }
    }
}
