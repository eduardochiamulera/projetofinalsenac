using Evian.Entities.Entities.Base;
using Evian.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Evian.Repository.Core
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : DomainBase
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> AllWithInactive
        {
            get
            {
                return dbSet;
            }
        }

        public List<EntityEntry> GetAffectEntries()
        {
            List<EntityEntry> entries = new List<EntityEntry>();
            foreach (var entry in context.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified))
                entries.Add(entry);

            return entries;
        }

        public IQueryable<TEntity> AllWithInactiveIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = this.dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<TEntity> All
        {
            get
            {
                return dbSet.Where(x => x.Ativo);
            }
        }

        public IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = this.dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public virtual TEntity Find(object id)
        {
            var obj = dbSet.Find(id);
            
            if (obj != null && obj.Ativo)
                return obj;
            
            return null;
        }

        public virtual ValueTask<TEntity> FindAsync(object id)
        {
            return dbSet.FindAsync(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            var result = dbSet.SingleOrDefault(p => p.Id.Equals(entity.Id));
            if (result != null)
            {
                try
                {
                    context.Entry(result).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
        }

        public virtual void Delete(Guid id)
        {
            var result = dbSet.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                result.Ativo = false;
                result.DataExclusao = DateTime.Now;
                result.UsuarioExclusao = "";
            }
        }

        public virtual void AttachForUpdate(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
        }

        public bool Exists(object primaryKey)
        {
            return Find(primaryKey) != null;
        }

        public virtual void DetachEntity(TEntity entityToDetach)
        {
            context.Entry(entityToDetach).State = EntityState.Detached;
        }

    }
}
