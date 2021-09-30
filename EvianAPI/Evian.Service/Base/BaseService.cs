using Evian.Entities.Base;
using Evian.Entities.DTO;
using Evian.Repository.Interfaces.Base;
using Evian.Service.Interface.Base;
using System;

namespace Evian.Service.Base
{
    public class BaseService<T, TVM> : IBaseService<T, TVM> where T : DomainBase where TVM : BaseDtO
    {
        protected readonly IRepository<T> _genericRepository;

        public BaseService(IRepository<T> repository)
        {
            _genericRepository = repository;
        }

        public TVM Create(T entity)
        {
            throw new Exception("Inclusão não permitida.");
        }

        public void Delete(Guid Id)
        {
            throw new Exception("Exclusão não permitida.");
        }

        public TVM FindAll()
        {
            throw new NotImplementedException();
        }

        public TVM FindById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public TVM Update(T entity)
        {
            throw new Exception("Atualização não permitida.");
        }
    }
}
