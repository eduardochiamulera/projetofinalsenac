using Evian.Entities.Entities;
using Evian.Entities.Enums;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class FormaPagamentoMap : EmpresaBaseMap<FormaPagamento> 
    {
        public FormaPagamentoMap() : base("forma_pagamento"){}

        public override void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired();
            builder.Property(x => x.TipoFormaPagamento).HasColumnName("tipo_forma_pagamento").IsRequired();
        }
    }
}
