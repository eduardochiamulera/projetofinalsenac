using Evian.Entities.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Domain
{
    public class CidadeMap : BaseDomainMap<Cidade>
    {
        public CidadeMap() : base("cidade"){}

        public override void Configure(EntityTypeBuilder<Cidade> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(200).IsRequired();
            builder.Property(x => x.CodigoIbge).HasColumnName("codigo_ibge").HasMaxLength(6).IsRequired();

            builder.Property(x => x.EstadoId).HasColumnName("estado_id").HasMaxLength(36).IsRequired();
            builder.HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.EstadoId);
        }
    }
}
