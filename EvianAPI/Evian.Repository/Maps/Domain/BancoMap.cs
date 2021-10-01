using Evian.Entities.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Domain
{
    public class BancoMap : BaseDomainMap<Banco>
    {
        public BancoMap() : base("banco"){}

        public override void Configure(EntityTypeBuilder<Banco> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Codigo).HasColumnName("codigo").HasMaxLength(3).IsRequired();
        }
    }
}
