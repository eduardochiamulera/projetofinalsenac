using Evian.Entities.Entities;
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

            builder.Property(x => x.ContaFinanceiraId).HasColumnName("conta_financeira_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaFinanceira).WithMany().HasForeignKey(x => x.ContaFinanceiraId);

            builder.Property(x => x.ContaBancariaId).HasColumnName("conta_bancaria_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaBancaria).WithMany().HasForeignKey(x => x.ContaBancariaId);
        }
    }
}
