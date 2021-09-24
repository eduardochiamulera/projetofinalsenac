using Evian.Entities;
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

            builder.Property(x => x.ContaPagarId).HasColumnName("conta_pagar_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaPagar).WithMany().HasForeignKey(x => x.ContaPagarId);

            builder.Property(x => x.ContaReceberId).HasColumnName("conta_receber_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaReceber).WithMany().HasForeignKey(x => x.ContaReceberId);

            builder.Property(x => x.ContaFinanceiraBaixaId).HasColumnName("conta_financeira_baixa_id").HasMaxLength(36).IsRequired();
        }
    }
}
