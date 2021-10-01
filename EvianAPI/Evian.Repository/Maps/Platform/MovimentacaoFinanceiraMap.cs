using Evian.Entities.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class MovimentacaoFinanceiraMap : EmpresaBaseMap<MovimentacaoFinanceira> 
    {
        public MovimentacaoFinanceiraMap() : base("movimentacao"){}

        public override void Configure(EntityTypeBuilder<MovimentacaoFinanceira> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Valor).HasColumnName("valor").IsRequired();
            builder.Property(x => x.Data).HasColumnName("data").IsRequired();

            builder.Property(x => x.ContaBancariaOrigemId).HasColumnName("conta_bancaria_origem_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaBancariaOrigem).WithMany().HasForeignKey(x => x.ContaBancariaOrigemId);

            builder.Property(x => x.ContaBancariaDestinoId).HasColumnName("conta_bancaria_destino_id").HasMaxLength(36);
            builder.HasOne(x => x.ContaBancariaDestino).WithMany().HasForeignKey(x => x.ContaBancariaDestinoId);

            builder.Property(x => x.ContaFinanceiraId).HasColumnName("conta_financeira_id").HasMaxLength(36);
        }
    }
}
