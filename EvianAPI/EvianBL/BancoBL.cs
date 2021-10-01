using Evian.Entities.Entities;
using Evian.Repository.Core;

namespace EvianBL
{
    public class BancoBL : GenericDomainBaseBL<Banco>
    {
        public BancoBL(ApplicationDbContext context) : base(context){ }
    }
}
