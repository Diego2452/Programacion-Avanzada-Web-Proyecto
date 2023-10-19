using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgramaciónAvanzadaWeb.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }
        public int IdUsuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        // Relación con Usuario
        public Usuario Usuario { get; set; }
    }
}
