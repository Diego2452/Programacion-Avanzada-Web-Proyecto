using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class CarrosImagenes
    {
        [Key]
        public int IdImagen { get; set; }

        [Required(ErrorMessage = "El campo IdCarro es obligatorio.")]
        [Display(Name = "Carro")]
        public int IdCarro { get; set; }

        [Required(ErrorMessage = "El campo Ruta de Imagen es obligatorio.")]
        [StringLength(255, ErrorMessage = "El campo Ruta de Imagen debe tener como máximo 255 caracteres.")]
        [Display(Name = "Ruta de Imagen")]
        public string ImagenPath { get; set; }

        [ForeignKey("IdCarro")]
        public Carros Carro { get; set; }
    }
}
