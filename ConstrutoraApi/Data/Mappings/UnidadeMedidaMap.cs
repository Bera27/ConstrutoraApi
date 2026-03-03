using ConstrutoraApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraApi.Data.Mappings
{
    public class UnidadeMedidaMap : IEntityTypeConfiguration<UnidadeMedida>
    {
        public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
        {
            builder.HasKey(um => um.Id);

            builder.Property(um => um.Id)
                .ValueGeneratedOnAdd();

            builder.Property(um => um.Sigla)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
