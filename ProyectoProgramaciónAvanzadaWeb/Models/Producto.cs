using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaVehiculos.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string NombreProducto { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }
        public int CantidadStock { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }

        // Relación con Categoría
        public Categoria Categoria { get; set; }
    }
}
