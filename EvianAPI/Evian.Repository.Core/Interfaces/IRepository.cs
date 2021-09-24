using Evian.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Evian.Repository.Core.Interface
{
    public interface IRepository<TEntity> where TEntity : DomainBase
    {
        IQueryable<TEntity> AllWithInactive { get; }

        IQueryable<TEntity> AllWithInactiveIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> All { get; }

        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        bool Any(Expression<Func<TEntity, bool>> predicate);

        TEntity Find(object id);

        ValueTask<TEntity> FindAsync(object id);

        void Insert(TEntity entity);

        void Delete(TEntity entityToDelete);

        void Delete(object id);

        void AttachForUpdate(TEntity entityToUpdate);

        bool Exists(object primaryKey);

        void DetachEntity(TEntity entityToDetach);
    }
}
