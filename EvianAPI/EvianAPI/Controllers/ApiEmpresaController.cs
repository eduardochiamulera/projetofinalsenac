using Evian.Entities.Base;
using EvianBL;

namespace EvianAPI.Controllers
{
    public class ApiEmpresaController<TEntity, TBL> : ApiDomainController<TEntity, TBL> 
        where TEntity : EmpresaBase, new()
        where TBL : EmpresaBL<TEntity>
    {
        protected void Insert(TEntity entity)
        {
            UnitOfWork.GetGenericBL<TBL>().Insert(entity);
        }

        protected void Update(TEntity entity)
        {
            UnitOfWork.GetGenericBL<TBL>().Update(entity);
        }

        protected void Delete(TEntity primaryKey)
        {
            UnitOfWork.GetGenericBL<TBL>().Delete(primaryKey);
        }
    }
}
