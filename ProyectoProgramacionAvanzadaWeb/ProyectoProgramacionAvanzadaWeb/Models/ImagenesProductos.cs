﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class ImagenesProductos
    {
        [Key]
        public int IdImagen { get; set; }

        [Required(ErrorMessage = "El campo IdProducto es obligatorio.")]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo Ruta de Imagen es obligatorio.")]
        [StringLength(255, ErrorMessage = "El campo Ruta de Imagen debe tener como máximo 255 caracteres.")]
        [Display(Name = "Ruta de Imagen")]
        public string ImagenPath { get; set; }
    }
}
