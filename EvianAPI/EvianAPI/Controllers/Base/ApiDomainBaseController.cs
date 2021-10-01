using AutoMapper;
using Evian.Entities.Entities.Base;
using EvianBL;
using System.Linq;
using System.Threading.Tasks;

namespace EvianAPI.Controllers.Base
{
    public class ApiDomainBaseController<TEntity, TBL> : ApiBaseController<TEntity> 
        where TEntity : DomainBase, new()
        where TBL : GenericDomainBaseBL<TEntity>
    {
        private UnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ApiDomainBaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected UnitOfWork UnitOfWork
        {
            get { return _unitOfWork ?? (_unitOfWork = new UnitOfWork(ContextInitialize)); }
            set { _unitOfWork = value; }
        }

        protected override bool Exists(object primaryKey)
        {
            return UnitOfWork.GetGenericBL<TBL>().Exists(primaryKey);
        }

        protected override IQueryable<TEntity> All()
        {
            return UnitOfWork.GetGenericBL<TBL>().All;
        }

        protected override TEntity Find(object id)
        {
            return UnitOfWork.GetGenericBL<TBL>().Find(id);
        }

        protected override void UnitDispose(bool disposing)
        {
            UnitOfWork?.Dispose();
        }

        protected async override Task UnitSave()
        {
            await UnitOfWork.Save();
        }
    }
}
