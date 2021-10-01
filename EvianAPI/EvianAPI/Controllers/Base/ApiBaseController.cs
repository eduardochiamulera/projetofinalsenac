using Evian.Entities.Entities.Base;
using Evian.Repository.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace EvianAPI.Controllers.Base
{
    public abstract class ApiBaseController<TEntity> : ControllerBase where TEntity : DomainBase, new()
    {
        protected abstract TEntity Find(object id);
        protected abstract bool Exists(object primaryKey); 

        protected abstract IQueryable<TEntity> All();

        protected abstract void UnitDispose(bool disposing);

        protected abstract Task UnitSave();

        private ContextInitialize _contextInitialize;
        protected ContextInitialize ContextInitialize
        {
            get
            {
                return _contextInitialize ?? (_contextInitialize = new ContextInitialize
                {
                    EmpresaId = EmpresaId,
                    AppUser = AppUser,
                });
            }
            set
            {
                _contextInitialize = value;
            }
        }

        public string AppUser
        {
            get
            {
                StringValues values;

                if (Request.Headers.TryGetValue("AppUser", out values))
                    return values.FirstOrDefault();

                throw new ArgumentException("AppUser não informado.");
            }
        }

        public Guid EmpresaId
        {
            get
            {
                StringValues values;

                // TODO: Resolver o problema quando o usuário envia duas vezes a plataforma fica no formato "plat1.fly01.com.br, plat1.fly01.com.br"
                if (Request.Headers.TryGetValue("EmpresaId", out values))
                    return Guid.Parse(values.FirstOrDefault());

                throw new ArgumentException("EmpresaId não informada.");
            }
        }
    }
}
