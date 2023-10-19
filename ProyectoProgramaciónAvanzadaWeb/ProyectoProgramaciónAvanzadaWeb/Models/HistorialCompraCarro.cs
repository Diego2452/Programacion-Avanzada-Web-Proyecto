using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgramaciónAvanzadaWeb.Models
{
    public class HistorialCompraCarro
    {
        [Key]
        public int IdHistorialCompraCarro { get; set; }
        public int IdUsuario { get; set; }
        public int IdCarro { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        // Relación con Usuario y Carro
        public Usuario Usuario { get; set; }
        public Carro Carro { get; set; }
    }
}
