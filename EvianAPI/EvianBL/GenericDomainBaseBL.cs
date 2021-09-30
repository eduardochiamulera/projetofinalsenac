using Evian.Entities.Base;
using Evian.Repository.Core;
using Evian.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EvianBL
{
    public class GenericDomainBaseBL<TEntity> where TEntity : DomainBase
    {
        protected readonly IRepository<TEntity> _repository;

        protected GenericDomainBaseBL(ApplicationDbContext context)
        {
            _repository = new GenericRepository<TEntity>(context);
        }

        public virtual IQueryable<TEntity> All => (IQueryable<TEntity>)_repository.All;

        public virtual IQueryable<TEntity> AllWithInactive => (IQueryable<TEntity>)_repository.AllWithInactive;

        public virtual IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.AllIncluding(includeProperties);
        }

        public virtual IQueryable<TEntity> AllWithInactiveIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.AllWithInactiveIncluding(includeProperties);
        }

        protected IQueryable<TEntity> AllFilterIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Filter(AllIncluding(includeProperties));
        }

        public TEntity Find(object id) => _repository.Find(id);

        protected void Insert(TEntity entity) => _repository.Insert(entity);

        public void Update(TEntity entityToUpdate) => _repository.Update(entityToUpdate);

        protected void Delete(TEntity entityToDelete, string confirmString)
        {
            _repository.Delete(entityToDelete);
        }

        protected void DetachEntity(TEntity entityToDetach)
        {
            _repository.DetachEntity(entityToDetach);
        }

        protected IQueryable<TEntity> Filter(IQueryable<TEntity> filterEntity)
        {
            IQueryable<TEntity> filteredTEntity = new List<TEntity>().AsQueryable();

            return filteredTEntity;
        }

        protected IEnumerable<TEntity> ContextAddedEntriesSelfType()
        {
            return _repository.GetAffectEntries().Where(x => x.State == EntityState.Added && x.Entity.GetType().Name == typeof(TEntity).Name)
                                            .Select(x => x.Entity as TEntity);
        }
    }
}
