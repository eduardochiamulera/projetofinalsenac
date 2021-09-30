using Evian.Entities.Base;
using Evian.Repository.Core;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EvianBL
{
    public class EmpresaBL<TEntity> : GenericDomainBaseBL<TEntity> where TEntity : EmpresaBase
    {
        protected readonly UnitOfWork _unitOfWork;
        private Expression<Func<TEntity, bool>> _predicateEmpresa { get; set; }
        private Guid _empresaId;
        public Guid EmpresaId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_empresaId.ToString()))
                    throw new Exception("ERRO!EmpresaId não informado.");

                return _empresaId;
            }
            set
            {
                _empresaId = value;
            }
        }

        private string _appUser;
        public string AppUser
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_appUser))
                    throw new Exception("ERRO! AppUser não informado.");

                return _appUser;
            }
            set
            {
                _appUser = value;
            }
        }
        protected EmpresaBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
            _predicateEmpresa = x => x.EmpresaId == context.EmpresaId && x.Ativo;
            AppUser = context.AppUser;
            EmpresaId = context.EmpresaId;
        }

        public virtual void ValidaModel(TEntity entity)
        {
            if (!entity.IsValid())
                throw new Exception(entity.Notification.Get());
        }

        public override IQueryable<TEntity> All => base.All.Where(_predicateEmpresa);

        public override IQueryable<TEntity> AllWithInactive => base.AllWithInactive.Where(x => x.EmpresaId == EmpresaId);

        public virtual IQueryable<TEntity> AllWithoutPlatform => base._repository.All;

        public override IQueryable<TEntity> AllWithInactiveIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return base.AllIncluding(includeProperties).Where(x => x.EmpresaId == EmpresaId);
        }

        public virtual new void Insert(TEntity entity)
        {
            entity.EmpresaId = EmpresaId;
            entity.DataInclusao = DateTime.Now;
            entity.DataAlteracao = null;
            entity.DataExclusao = null;
            entity.UsuarioInclusao = AppUser;
            entity.UsuarioAlteracao = null;
            entity.UsuarioExclusao = null;
            if (!entity.Ativo)
            {
                entity.UsuarioExclusao = AppUser;
                entity.DataExclusao = DateTime.Now;
            }


            ValidaModel(entity);

            if (entity.Id == default(Guid) || entity.Id == null)
                entity.Id = Guid.NewGuid();

            _repository.Insert(entity);
        }

        public virtual new void Update(TEntity entity)
        {
            entity.EmpresaId = EmpresaId;
            entity.DataAlteracao = DateTime.Now;
            entity.DataExclusao = null;
            entity.UsuarioAlteracao = AppUser;
            entity.UsuarioExclusao = null;
            if (!entity.Ativo)
            {
                entity.UsuarioExclusao = AppUser;
                entity.DataExclusao = DateTime.Now;
            }

            ValidaModel(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            entityToDelete.Ativo = false;
            entityToDelete.DataExclusao = DateTime.Now;
            entityToDelete.UsuarioExclusao = AppUser;

            _repository.Delete(entityToDelete);
        }
    }
}
