using Evian.Entities.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class CondicaoParcelamentoMap : EmpresaBaseMap<CondicaoParcelamento> 
    {
        public CondicaoParcelamentoMap() : base("condicao_parcelamento"){}

        public override void Configure(EntityTypeBuilder<CondicaoParcelamento> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired();
            builder.Property(x => x.QtdParcelas).HasColumnName("quantidade_parcelas");
            builder.Property(x => x.CondicoesParcelamento).HasColumnName("condicoes_parcelamento");

        }
    }
}
