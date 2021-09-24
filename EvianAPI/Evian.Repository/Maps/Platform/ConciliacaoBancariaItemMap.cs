using Evian.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class ConciliacaoBancariaItemMap : EmpresaBaseMap<ConciliacaoBancariaItem> 
    {
        public ConciliacaoBancariaItemMap() : base("conciliacao_bancaria_item"){}

        public override void Configure(EntityTypeBuilder<ConciliacaoBancariaItem> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Descricao).HasColumnName("descricao").IsRequired();
            builder.Property(x => x.Valor).HasColumnName("valor").IsRequired();
            builder.Property(x => x.Data).HasColumnName("data").IsRequired();
            builder.Property(x => x.OfxLancamentoMD5).HasColumnName("ofx_lancamento_md5").IsRequired();
            builder.Property(x => x.StatusConciliado).HasMaxLength(1).HasColumnName("status").IsRequired();
            
            builder.Property(x => x.ConciliacaoBancariaId).HasColumnName("conciliacao_bancaria_id").HasMaxLength(36).IsRequired();
            builder.HasOne(x => x.ConciliacaoBancaria).WithMany().HasForeignKey(x => x.ConciliacaoBancariaId);
        }
    }
}
