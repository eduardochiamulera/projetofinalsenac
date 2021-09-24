using Evian.Entities;
using Evian.Repository.Core;
using Evian.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evian.Repository.Implementation
{
    public class ConciliacaoBancariaRepository : GenericRepository<ConciliacaoBancaria>, IConciliacaoBancariaRepository
    {
        public ConciliacaoBancariaRepository(DbContext context) : base(context) { }

    }
}
