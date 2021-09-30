using Evian.Entities;
using Evian.Repository.Core;
using EvianBL;

namespace EvianBL
{
    public class EstadoBL : GenericDomainBaseBL<Estado>
    {
        public EstadoBL(ApplicationDbContext context) : base(context)
        { }
    }
}
