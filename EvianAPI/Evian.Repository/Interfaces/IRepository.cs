using Evian.Entities.Entities.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Evian.Repository.Interfaces.Base
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

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entityToDelete);

        void Delete(Guid id);

        bool Exists(object primaryKey);

        void DetachEntity(TEntity entityToDetach);

        List<EntityEntry> GetAffectEntries();
    }
}
