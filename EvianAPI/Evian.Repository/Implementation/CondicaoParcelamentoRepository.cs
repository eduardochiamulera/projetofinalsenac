using Evian.Entities;
using Evian.Repository.Core;
using Evian.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Evian.Repository.Implementation
{
    public class CondicaoParcelamentoRepository : GenericRepository<CondicaoParcelamento>, ICondicaoParcelamentoRepository
    {
        public CondicaoParcelamentoRepository(DbContext context) : base(context) { }

    }
}
