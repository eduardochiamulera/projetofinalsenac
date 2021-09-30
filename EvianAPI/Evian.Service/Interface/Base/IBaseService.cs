using Evian.Entities.Base;
using Evian.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evian.Service.Interface.Base
{
    public interface IBaseService<T, TVM> where T : DomainBase where TVM : BaseDtO
    {
        TVM Create(T entity);

        TVM Update(T entity);

        TVM FindAll();

        TVM FindById(Guid Id);

        void Delete(Guid Id);
    }
}
