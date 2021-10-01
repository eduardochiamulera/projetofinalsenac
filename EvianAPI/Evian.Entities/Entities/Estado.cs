using Evian.Entities.Entities.Base;
using System;

namespace Evian.Entities.Entities
{
    public class Estado : DomainBase
    {
        public string Sigla { get; set; }

        public string Nome { get; set; }

        public string UtcId { get; set; }

        public string CodigoIbge { get; set; }

        public Guid PaisId { get; set; }

        public virtual Pais Pais { get; set; }
    }
}