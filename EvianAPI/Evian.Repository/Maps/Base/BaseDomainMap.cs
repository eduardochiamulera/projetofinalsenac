using Evian.Entities.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Base
{
    public class BaseDomainMap<T> : IEntityTypeConfiguration<T> where T : DomainBase
    {
        private readonly string _tableName;

        public BaseDomainMap(string tableName = "")
        {
            _tableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if (!string.IsNullOrWhiteSpace(_tableName))
            {
                builder.ToTable(_tableName);
            }

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.DataInclusao).HasColumnName("data_inclusao").IsRequired();
            builder.Property(x => x.DataAlteracao).HasColumnName("data_alteracao");
            builder.Property(x => x.DataExclusao).HasColumnName("data_exclusao");
            builder.Property(x => x.UsuarioInclusao).HasColumnName("usuario_inclusao").HasMaxLength(200).IsRequired();
            builder.Property(x => x.UsuarioAlteracao).HasColumnName("usuario_alteracao").HasMaxLength(200);
            builder.Property(x => x.UsuarioExclusao).HasColumnName("usuario_exclusao").HasMaxLength(200);
            builder.Property(x => x.Ativo).HasColumnName("ativo").IsRequired();


        }
    }
}
