
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class CarritoDeCompras
    {
        [Key]
        public int IdCarrito { get; set; }

        [Required(ErrorMessage = "El campo IdProducto es obligatorio.")]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo Cantidad de Producto es obligatorio.")]
        [Display(Name = "Cantidad de Producto")]
        public int CantidadProducto { get; set; }

        [Required(ErrorMessage = "El campo Precio Unitario es obligatorio.")]
        [Range(0.01, 1000000.00, ErrorMessage = "El campo Precio Unitario debe estar entre 0.01 y 1,000,000.00")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El campo IdEstado es obligatorio.")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El campo Total es obligatorio.")]
        [Range(0.01, 1000000.00, ErrorMessage = "El campo Total debe estar entre 0.01 y 1,000,000.00")]
        [DataType(DataType.Currency)]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Creación es obligatorio.")]
        [Display(Name = "Fecha de Creación")]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "El campo IdUsuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdEstado")]
        public Estados? Estado { get; set; }

        [ForeignKey("IdProducto")]
        public Productos? Producto { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios? Usuario { get; set; }
    }
}
