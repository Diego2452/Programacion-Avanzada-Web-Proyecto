using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class TipoTransmisiones
    {
        [Key]
        public int IdTransmision { get; set; }

        [Required(ErrorMessage = "El campo TipoTransmision es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo TipoTransmision debe tener como máximo 30 caracteres.")]
        [Display(Name = "Tipo de Transmisión")]
        public string TipoTransmision { get; set; }
    }
}
