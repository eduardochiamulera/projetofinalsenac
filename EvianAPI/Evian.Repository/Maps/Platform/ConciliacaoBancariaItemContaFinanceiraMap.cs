using Evian.Entities.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class ConciliacaoBancariaItemContaFinanceiraMap : EmpresaBaseMap<ConciliacaoBancariaItemContaFinanceira> 
    {
        public ConciliacaoBancariaItemContaFinanceiraMap() : base("conciliacao_bancaria_item_conta_financeira"){}

        public override void Configure(EntityTypeBuilder<ConciliacaoBancariaItemContaFinanceira> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.ValorConciliado).HasColumnName("valor_conciliado").IsRequired();
            
            builder.Property(x => x.ConciliacaoBancariaItemId).HasColumnName("conciliacao_bancaria_id").HasMaxLength(36).IsRequired();
            builder.HasOne(x => x.ConciliacaoBancariaItem).WithOne().HasForeignKey<ConciliacaoBancariaItem>();

            builder.Property(x => x.ContaFinanceiraId).HasColumnName("conta_financeira_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaFinanceira).WithMany().HasForeignKey(x => x.ContaFinanceiraId);

            builder.Property(x => x.ContaFinanceiraBaixaId).HasColumnName("conta_financeira_baixa_id").HasMaxLength(36).IsRequired();
        }
    }
}
