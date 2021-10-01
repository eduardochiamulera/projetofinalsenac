using Evian.Entities.Entities.Base;
using Evian.Notifications;
using EvianBL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

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

        protected Notification Notification { get; } = new Notification();

        protected void AddErrorModelState(ModelStateDictionary modelState)
        {
            modelState.ToList().ForEach(
                model => model.Value.Errors.ToList().ForEach(
                    itemErro => Notification.Errors.Add(
                        new Error(itemErro.ErrorMessage, string.Concat(char.ToLowerInvariant(model.Key[0]), model.Key.Substring(1))))));

            throw new Exception(Notification.Get());
        }
    }
}
