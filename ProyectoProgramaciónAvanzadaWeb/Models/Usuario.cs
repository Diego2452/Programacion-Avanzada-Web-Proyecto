using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgramaciónAvanzadaWeb.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string? Contraseña { get; set; }
        public string? Rol { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Cedula { get; set; }
        public string? Sexo { get; set; }
        public string? NumeroTelefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Direccion { get; set; }
    }
}
