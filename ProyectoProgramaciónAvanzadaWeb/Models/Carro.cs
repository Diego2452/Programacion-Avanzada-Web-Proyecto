using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgramaciónAvanzadaWeb.Models
{
    public class Carro
    {
        [Key]
        public int IdCarro { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string Imagen4 { get; set; }
        public string Imagen5 { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Estilo { get; set; }
        public int Año { get; set; }
        public string TipoCombustible { get; set; }
        public string TipoTransmision { get; set; }
        public int NumPuertas { get; set; }
        public string ColorExterior { get; set; }
        public string ColorInterior { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Placa { get; set; }
        public string TipoFinanciamiento { get; set; }
        public bool Apartado { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        // Relación con HistorialCompraCarro
        public ICollection<HistorialCompraCarro> HistorialCompraCarros { get; set; }
    }
}
