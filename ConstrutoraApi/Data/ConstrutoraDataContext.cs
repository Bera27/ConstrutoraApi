using ConstrutoraApi.Data.Mappings;
using ConstrutoraApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraApi.Data
{
    public class ConstrutoraDataContext : DbContext
    {
        public DbSet<OrcamentoObra> OrcamentosObras { get; set; }
        public DbSet<PlanilhaObra> PlanilhaObra { get; set; }

        public ConstrutoraDataContext(DbContextOptions<ConstrutoraDataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrcamentoObraMap());
            modelBuilder.ApplyConfiguration(new PlanilhaObraMap());
        }
    }
}
