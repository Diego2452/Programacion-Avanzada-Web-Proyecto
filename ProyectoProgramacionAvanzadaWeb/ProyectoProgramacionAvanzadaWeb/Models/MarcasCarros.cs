using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class MarcasCarros
    {
        [Key]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El campo Marca es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Marca debe tener como máximo 30 caracteres.")]
        public string Marca { get; set; }
    }
}
