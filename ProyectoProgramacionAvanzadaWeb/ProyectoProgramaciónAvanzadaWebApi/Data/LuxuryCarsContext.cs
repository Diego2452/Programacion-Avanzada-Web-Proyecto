using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Models;
using System.Collections.Generic;

namespace ProyectoProgramacionAvanzadaWebApi.Data
{
    public class LuxuryCarsContext: DbContext
    {
        public LuxuryCarsContext(DbContextOptions<LuxuryCarsContext> options): base(options)
        {
        }
        public DbSet<CarritoDeCompras> CarritoDeCompras { get; set; }
        public DbSet<Carros> Carros { get; set; }
        public DbSet<CarrosImagenes> CarrosImagenes { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Facturacion> Facturacion { get; set; }
        public DbSet<HistorialCompraCarro> HistorialCompraCarro { get; set; }
        public DbSet<ImagenesProductos> ImagenesProductos { get; set; }
        public DbSet<MarcasCarros> MarcasCarros { get; set; }
        public DbSet<MetodosDePago> MetodosDePago { get; set; }
        public DbSet<ModelosCarros> ModelosCarros { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Sexo> Sexo { get; set; }
        public DbSet<TipoIdentificaciones> TipoIdentificaciones { get; set; }
        public DbSet<TipoCombustibles> TipoCombustibles { get; set; }
        public DbSet<TipoFinanciamientos> TipoFinanciamientos { get; set; }
        public DbSet<TipoTransmisiones> TipoTransmisiones { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
