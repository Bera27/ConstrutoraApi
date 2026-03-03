using ConstrutoraApi.Data.Mappings;
using ConstrutoraApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraApi.Data
{
    public class ConstrutoraDataContext : DbContext
    {
        public DbSet<OrcamentoObra> OrcamentosObras { get; set; }
        public DbSet<UnidadeMedida> UnidadeMedidas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrcamentoObraMap());
            modelBuilder.ApplyConfiguration(new UnidadeMedidaMap());
        }
    }
}
