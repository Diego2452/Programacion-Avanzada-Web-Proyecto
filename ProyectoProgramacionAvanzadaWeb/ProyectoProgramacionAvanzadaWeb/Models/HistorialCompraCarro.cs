﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgramacionAvanzadaWeb.Models
{
    public class HistorialCompraCarro
    {
        [Key]
        public int IdHistorialCompraCarro { get; set; }

        [Required(ErrorMessage = "El campo IdUsuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo IdCarro es obligatorio.")]
        [Display(Name = "Carro")]
        public int IdCarro { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [StringLength(300, ErrorMessage = "El campo Descripción debe tener como máximo 300 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo IdEstado es obligatorio.")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios? Usuario { get; set; }

        [ForeignKey("IdCarro")]
        public Carros? Carro { get; set; }

        [ForeignKey("IdEstado")]
        public Estados? Estado { get; set; }
    }
}
