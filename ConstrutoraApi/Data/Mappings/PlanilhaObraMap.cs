using ConstrutoraApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraApi.Data.Mappings
{
    public class PlanilhaObraMap : IEntityTypeConfiguration<PlanilhaObra>
    {
        public void Configure(EntityTypeBuilder<PlanilhaObra> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Nome)
                .HasMaxLength(200);
        }
    }
}