using Evian.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Base
{
    public class EmpresaBaseMap<T> : BaseDomainMap<T> where T : EmpresaBase
    {
        public EmpresaBaseMap(string tableName = "") : base(tableName){}

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.EmpresaId).HasMaxLength(36).HasColumnName("empresa_id").IsRequired();
        }
    }
}
