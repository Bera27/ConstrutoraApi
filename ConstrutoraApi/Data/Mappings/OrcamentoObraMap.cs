using ConstrutoraApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraApi.Data.Mappings
{
    public class OrcamentoObraMap : IEntityTypeConfiguration<OrcamentoObra>
    {
        public void Configure(EntityTypeBuilder<OrcamentoObra> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Codigo)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(20);

            builder.Property(o => o.Item)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(o => o.Servico)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.Descricao)
                .HasMaxLength(255);

            builder.Property(o => o.UnidadeMedida)
                .HasColumnType("nvarchar")
                .HasMaxLength(25);

            builder.Property(o => o.Qtd)
                .HasColumnType("decimal(18,4)");

            builder.Property(o => o.CustoMat)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.CustoMO)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.CustoEquip)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.CustoUnitTotal)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.CustoTotal)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Bdi)
                .HasColumnType("decimal(5,2)");

            builder.Property(o => o.PrecoUnit)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.PrecoTotal)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Peso)
                .HasColumnType("decimal(18,4)");
        }
    }
}
