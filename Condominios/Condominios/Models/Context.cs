using Condominios.Models.Entities;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618

namespace Condominios.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {  }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {  }
        
        public virtual DbSet<Equipo> Equipo { get; set; }
        public virtual DbSet<Estatus> Estatus { get; set; }
        public virtual DbSet<Mantenimiento> Mantenimiento { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Motor> Motor { get; set; }
        public virtual DbSet<MtoProgramado> MtoProgramado { get; set; }
        public virtual DbSet<Periodo> Periodo { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<TipoMantenimiento> TipoMantenimiento { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; } 

        public virtual DbSet<Funcion> Funcion { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<TipoEquipo> TipoEquipo { get; set; }

        public virtual DbSet<Variante> Variante { get; set; }

        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
