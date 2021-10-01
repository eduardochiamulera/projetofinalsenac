using Evian.Entities.Entities;
using Evian.Entities.Entities.Enums;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class CategoriaMap : EmpresaBaseMap<Categoria> 
    {
        public CategoriaMap() : base("categoria"){}

        public override void Configure(EntityTypeBuilder<Categoria> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired();
            builder.Property(x => x.TipoCarteira).HasColumnName("tipo_carteira").HasMaxLength(1).IsRequired();

            builder.Property(x => x.CategoriaPaiId).HasColumnName("categoria_pai_id").HasMaxLength(36);
            builder.HasOne(x => x.CategoriaPai).WithMany().HasForeignKey(x => x.CategoriaPaiId);
        }
    }
}
