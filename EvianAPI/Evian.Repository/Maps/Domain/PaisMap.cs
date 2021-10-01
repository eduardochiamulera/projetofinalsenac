using Evian.Entities.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Domain
{
    public class PaisMap : BaseDomainMap<Pais>
    {
        public PaisMap() : base("pais"){}

        public override void Configure(EntityTypeBuilder<Pais> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Sigla).HasColumnName("sigla").HasMaxLength(2).IsRequired();
            builder.Property(x => x.CodigoIbge).HasColumnName("codigo_ibge").HasMaxLength(6).IsRequired();
        }
    }
}
