using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Data
{
    public class ProyectoProgramacionAvanzadaWebContext : DbContext
    {
        public ProyectoProgramacionAvanzadaWebContext (DbContextOptions<ProyectoProgramacionAvanzadaWebContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.Usuarios> Usuarios { get; set; } = default!;
    }
}
