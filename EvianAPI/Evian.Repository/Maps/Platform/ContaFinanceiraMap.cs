using Evian.Entities.Entities;
using Evian.Entities.Enums;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class ContaFinanceiraMap : EmpresaBaseMap<ContaFinanceira> 
    {
        public ContaFinanceiraMap() : base("conta_financeira") {}
        public override void Configure(EntityTypeBuilder<ContaFinanceira> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.ContaFinanceiraRepeticaoPaiId).HasColumnName("conta_pagar_pai_id").HasMaxLength(36);
            //builder.HasOne(x => x.ContaFinanceiraParcelaPai).WithOne(x => x.ContaFinanceiraParcela).HasForeignKey<ContaFinanceira>(x => x.conta)


            builder.Property(x => x.ContaFinanceiraParcelaPaiId).HasColumnName("conta_pagar_parcela_pai_id").HasMaxLength(36);

            builder.Property(x => x.ValorPrevisto).HasColumnName("valor_previsto").IsRequired();
            builder.Property(x => x.ValorPago).HasColumnName("valor_pago");
            builder.Property(x => x.Saldo).HasColumnName("saldo");

            builder.Property(x => x.CategoriaId).HasColumnName("categoria_id").HasMaxLength(36);
            builder.HasOne(x => x.Categoria).WithMany().HasForeignKey(x => x.CategoriaId);

            builder.Property(x => x.CondicaoParcelamentoId).HasColumnName("condicao_parcelamento_id").HasMaxLength(36);
            builder.HasOne(x => x.CondicaoParcelamento).WithMany().HasForeignKey(x => x.CondicaoParcelamentoId);

            builder.Property(x => x.FormaPagamentoId).HasColumnName("forma_pagamento_id").HasMaxLength(36);
            builder.HasOne(x => x.FormaPagamento).WithMany().HasForeignKey(x => x.FormaPagamentoId);

            builder.Property(x => x.PessoaId).HasColumnName("pessoa_id").HasMaxLength(36);
            builder.HasOne(x => x.Pessoa).WithMany().HasForeignKey(x => x.PessoaId);

            builder.Property(x => x.DataEmissao).HasColumnName("data_emissao").IsRequired();
            builder.Property(x => x.DataVencimento).HasColumnName("data_vencimento").IsRequired();

            builder.Property(x => x.Descricao).HasMaxLength(200).HasColumnName("descricao").IsRequired();
            builder.Property(x => x.Observacao).HasMaxLength(800).HasColumnName("observacao").IsRequired();
            builder.Property(x => x.TipoContaFinanceira).HasColumnName("tipo_conta_financeira").IsRequired();
            builder.Property(x => x.StatusContaBancaria).HasColumnName("status").IsRequired();
            builder.Property(x => x.TipoPeriodicidade).HasColumnName("tipo_periodicidade").IsRequired();
            builder.Property(x => x.NumeroRepeticoes).HasColumnName("numero_repeticoes");
            builder.Property(x => x.Repetir).HasColumnName("repetir").IsRequired();
            builder.Property(x => x.Numero).HasColumnName("numero").IsRequired().ValueGeneratedOnAddOrUpdate();

            builder.HasOne(x => x.MovimentacaoFinanceira).WithOne(x => x.ContaFinanceira).HasForeignKey<MovimentacaoFinanceira>(x => x.ContaFinanceiraId);

        }
    }
}
