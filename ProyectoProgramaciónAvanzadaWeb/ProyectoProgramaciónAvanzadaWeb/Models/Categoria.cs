using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgramaciónAvanzadaWeb.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string TipoCategoria { get; set; }
    }
}
