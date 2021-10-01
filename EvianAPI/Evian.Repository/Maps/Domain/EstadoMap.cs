using Evian.Entities.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Domain
{
    public class EstadoMap : BaseDomainMap<Estado>
    {
        public EstadoMap() : base("estado"){}

        public override void Configure(EntityTypeBuilder<Estado> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Sigla).HasColumnName("sigla").HasMaxLength(2).IsRequired();
            builder.Property(x => x.CodigoIbge).HasColumnName("codigo_ibge").HasMaxLength(6).IsRequired();

            builder.Property(x => x.PaisId).HasColumnName("pais_id").HasMaxLength(36).IsRequired();
            builder.HasOne(x => x.Pais).WithMany().HasForeignKey(x => x.PaisId);
        }
    }
}
