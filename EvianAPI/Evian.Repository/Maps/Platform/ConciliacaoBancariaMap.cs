using Evian.Entities.Entities;
using Evian.Entities.Enums;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class ConciliacaoBancariaMap : EmpresaBaseMap<ConciliacaoBancaria> 
    {
        public ConciliacaoBancariaMap() : base("conciliacao_bancaria"){}

        public override void Configure(EntityTypeBuilder<ConciliacaoBancaria> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Arquivo).HasColumnName("arquivo").IsRequired();
            builder.Property(x => x.ContaBancariaId).HasColumnName("conta_bancaria_id").HasMaxLength(36).IsRequired();

            builder.HasOne(x => x.ContaBancaria).WithMany().HasForeignKey(x => x.ContaBancariaId);
        }
    }
}
