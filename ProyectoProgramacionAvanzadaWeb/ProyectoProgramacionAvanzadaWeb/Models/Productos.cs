using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo IdCategoría es obligatorio.")]
        [Display(Name = "Categoría")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo IdProveedor es obligatorio.")]
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "El campo Nombre del Producto es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Nombre del Producto debe tener como máximo 30 caracteres.")]
        [Display(Name = "Nombre del Producto")]
        public string NombreProducto { get; set; }

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0.01, 1000000.00, ErrorMessage = "El campo Precio debe estar entre 0.01 y 1,000,000.00")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo Cantidad en Stock es obligatorio.")]
        [Display(Name = "Cantidad en Stock")]
        public int CantidadStock { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [StringLength(300, ErrorMessage = "El campo Descripción debe tener como máximo 300 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Estado es obligatorio.")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [ForeignKey("IdProveedor")]
        public Proveedores? Proveedor { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; }
        [ForeignKey("IdProducto")]
        public ICollection<ImagenesProductos>? Imagenes { get; set; }
    }
}
