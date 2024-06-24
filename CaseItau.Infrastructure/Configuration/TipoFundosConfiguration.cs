using CaseItau.Domain.Fundos.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseItau.Infrastructure.Configuration
{
    public class TipoFundosConfiguration : IEntityTypeConfiguration<TipoFundo>
    {
        public void Configure(EntityTypeBuilder<TipoFundo> builder)
        {
            builder.ToTable("TIPO_FUNDO");

            builder.HasKey(x => x.Codigo);
            builder.Property(f => f.Codigo)
                   .HasColumnType("INTEGER")
                   .IsRequired();

            builder.Property(f => f.Nome)
                   .HasColumnType("VARCHAR(20)")
                   .IsRequired();
        }
    }
}