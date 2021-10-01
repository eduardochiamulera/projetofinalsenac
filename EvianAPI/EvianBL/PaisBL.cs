using Evian.Entities.Entities;
using Evian.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EvianBL
{
    public class PaisBL : GenericDomainBaseBL<Pais>
    {
        private readonly ApplicationDbContext _context;

        public PaisBL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Pais FindByNome(string nome)
        {
            var paises = _context.Set<Pais>()
                .Where(x => x.Nome.ToUpper().Equals(nome.ToUpper())).AsNoTracking().ToListAsync().Result;

            if (paises == null || !paises.Any())
            {
                return null;
            }

            return paises[0];
        }
    }
}
