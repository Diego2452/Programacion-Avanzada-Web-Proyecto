using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class ModelosCarros
    {
        [Key]
        public int? IdModelo { get; set; }

        [Required(ErrorMessage = "El campo IdMarca es obligatorio.")]
        public int? IdMarca { get; set; }

        [Required(ErrorMessage = "El campo Modelo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Modelo debe tener como máximo 50 caracteres.")]
        public string? Modelo { get; set; }

        [ForeignKey("IdMarca")]
        public MarcasCarros? Marca { get; set; }
    }
}