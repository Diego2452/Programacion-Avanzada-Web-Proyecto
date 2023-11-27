using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Data
{
    public class ProyectoProgramacionAvanzadaWebContext : DbContext
    {
        public ProyectoProgramacionAvanzadaWebContext(DbContextOptions<ProyectoProgramacionAvanzadaWebContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.Usuarios> Usuarios { get; set; } = default!;

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.TipoIdentificaciones>? TipoIdentificaciones { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.Roles>? Roles { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.Genero>? Sexo { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.LoginRequest>? LoginRequest { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.Carros>? Carros { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.Categoria>? Categoria { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.Productos>? Productos { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.Proveedores>? Proveedores { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.MarcasCarros>? MarcasCarros { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.ModelosCarros>? ModelosCarros { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.TipoCombustibles>? TipoCombustibles { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.TipoFinanciamientos>? TipoFinanciamientos { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.TipoTransmisiones>? TipoTransmisiones { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.MetodosDePago>? MetodosDePago { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.HistorialCompraCarro>? HistorialCompraCarro { get; set; }

        public DbSet<ProyectoProgramacionAvanzadaWeb.Models.Facturacion>? Facturacion { get; set; }
    }
}
