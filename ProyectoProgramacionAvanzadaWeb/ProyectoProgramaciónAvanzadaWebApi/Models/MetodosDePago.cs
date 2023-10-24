using System.ComponentModel.DataAnnotations;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class MetodosDePago
    {
        [Key]
        public int IdMetodoPago { get; set; }

        [Required(ErrorMessage = "El campo Nombre del Método de Pago es obligatorio.")]
        [StringLength(255, ErrorMessage = "El campo Nombre del Método de Pago debe tener como máximo 255 caracteres.")]
        [Display(Name = "Nombre del Método de Pago")]
        public string NombreMetodo { get; set; } //Valor unico
    }
}
