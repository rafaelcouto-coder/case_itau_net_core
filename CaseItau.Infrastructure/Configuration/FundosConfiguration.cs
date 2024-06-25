using CaseItau.Domain.Fundos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseItau.Infrastructure.Configuration;

public class FundosConfiguration : IEntityTypeConfiguration<Fundo>
{
    public void Configure(EntityTypeBuilder<Fundo> builder)
    {
        builder.ToTable("FUNDO");

        builder.HasKey(x => x.Codigo);
        builder.Property(f => f.Codigo)
               .HasColumnType("VARCHAR(20)")
               .IsRequired();

        builder.Property(f => f.Nome)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.Property(f => f.Cnpj)
               .HasColumnType("VARCHAR(14)")
               .IsRequired();

        builder.Property(f => f.Patrimonio)
               .HasColumnType("NUMERIC")
               .IsRequired(false);

        builder.Property(f => f.TipoFundoId)
                   .HasColumnType("INTEGER")
                   .IsRequired()
                   .HasColumnName("CODIGO_TIPO"); ;

        builder.HasOne(f => f.TipoFundo)
               .WithMany()
               .HasForeignKey(f => f.TipoFundoId)
               .HasConstraintName("FK_Fundo_TipoFundo");
    }
}
