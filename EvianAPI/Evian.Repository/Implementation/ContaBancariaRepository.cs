using Evian.Entities;
using Evian.Repository.Core;
using Evian.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Evian.Repository.Implementation
{
    public class ContaBancariaRepository : GenericRepository<ContaBancaria>, IContaBancariaRepository
    {
        public ContaBancariaRepository(DbContext context) : base(context) { }

    }
}
