using Evian.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class ContaFinanceiraBaixaMap : EmpresaBaseMap<ContaFinanceiraBaixa> 
    {
        public ContaFinanceiraBaixaMap() : base("conta_financeira_baixa"){}

        public override void Configure(EntityTypeBuilder<ContaFinanceiraBaixa> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Data).HasColumnName("data").IsRequired();

            builder.Property(x => x.Valor).HasColumnName("valor").IsRequired();

            builder.Property(x => x.Observacao).HasColumnName("observacao").HasMaxLength(800);

            builder.Property(x => x.ContaPagarId).HasColumnName("conta_pagar_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaPagar).WithMany().HasForeignKey(x => x.ContaPagarId);

            builder.Property(x => x.ContaReceberId).HasColumnName("conta_receber_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaReceber).WithMany().HasForeignKey(x => x.ContaReceberId);

            builder.Property(x => x.ContaBancariaId).HasColumnName("conta_bancaria_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaBancaria).WithMany().HasForeignKey(x => x.ContaBancariaId);
        }
    }
}
