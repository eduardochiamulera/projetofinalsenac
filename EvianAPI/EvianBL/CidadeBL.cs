using Evian.Entities.Entities;
using Evian.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EvianBL
{
    public class CidadeBL : GenericDomainBaseBL<Cidade>
    {
        private readonly ApplicationDbContext _context;

        public CidadeBL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Cidade FindByNome(string nome)
        {
            var cidades = _context.Set<Cidade>()
                .Where(x => x.Nome.ToUpper().Equals(nome.ToUpper())).AsNoTracking().ToListAsync().Result;

            if (cidades == null || !cidades.Any())
            {
                return null;
            }

            return cidades[0];
        }
    }
}
