using Evian.Entities.Base;
using Evian.Entities.DTO;
using Evian.Repository.Interfaces.Base;
using System;

namespace Evian.Service.Base
{
    public class EmpresaBaseService<T, TVM> : BaseService<T, TVM> where T : EmpresaBase where TVM : BaseDtO
    {

        public EmpresaBaseService(IRepository<T> repository = null) : base(repository){}

        public TVM Create(T entity)
        {
             _genericRepository.Insert(entity);
            return entity as TVM;
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
