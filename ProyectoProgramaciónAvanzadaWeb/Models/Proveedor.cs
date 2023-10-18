using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaVehiculos.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string UbicacionEmpresa { get; set; }
        public string CorreoEmpresa { get; set; }
        public string NumeroTelefonoEmpresa { get; set; }
        public string MarcasProveedor { get; set; }
    }
}
