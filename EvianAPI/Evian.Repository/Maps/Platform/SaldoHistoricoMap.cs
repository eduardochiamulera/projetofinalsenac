using Evian.Entities;
using Evian.Entities.Enums;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class SaldoHistoricoMap : EmpresaBaseMap<SaldoHistorico> 
    {
        public SaldoHistoricoMap() : base("saldo_historico"){}

        public override void Configure(EntityTypeBuilder<SaldoHistorico> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Data).HasColumnName("data").IsRequired();
            builder.Property(x => x.SaldoDia).HasColumnName("saldo_dia").IsRequired();

            builder.Property(x => x.SaldoConsolidado).HasColumnName("saldo_consolidado").IsRequired();

            builder.Property(x => x.TotalRecebimentos).HasColumnName("total_recebimentos").IsRequired();
            
            builder.Property(x => x.TotalPagamentos).HasColumnName("total_pagamentos").IsRequired();

            builder.Property(x => x.ContaBancariaId).HasMaxLength(36).HasColumnName("total_pagamentos").IsRequired();
        }
    }
}
