using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgramacionAvanzadaWebApi.Models
{
    public class Facturacion
    {
        [Key]
        public int IdFactura { get; set; }

        [Required(ErrorMessage = "El campo IdUsuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [StringLength(300, ErrorMessage = "El campo Descripción debe tener como máximo 300 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo IdEstado es obligatorio.")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El campo Total de la Factura es obligatorio.")]
        [Range(0.01, 1000000.00, ErrorMessage = "El campo Total de la Factura debe estar entre 0.01 y 1,000,000.00")]
        [DataType(DataType.Currency)]
        [Display(Name = "Total de la Factura")]
        public decimal TotalFactura { get; set; }

        [Required(ErrorMessage = "El campo Dirección de Envío es obligatorio.")]
        [StringLength(300, ErrorMessage = "El campo Dirección de Envío debe tener como máximo 300 caracteres.")]
        [Display(Name = "Dirección de Envío")]
        public string DireccionEnvio { get; set; }

        [Required(ErrorMessage = "El campo IdMetodoPago es obligatorio.")]
        [Display(Name = "Método de Pago")]
        public int IdMetodoPago { get; set; }

        [Required(ErrorMessage = "El campo Número de Factura es obligatorio.")]
        [StringLength(20, ErrorMessage = "El campo Número de Factura debe tener como máximo 20 caracteres.")]
        [Display(Name = "Número de Factura")]
        public string NumeroFactura { get; set; }

        [ForeignKey("IdEstado")]
        public Estados? Estado { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios? Usuario { get; set; }

        [ForeignKey("IdMetodoPago")]
        public MetodosDePago? MetodoPago { get; set; }
    }
}
