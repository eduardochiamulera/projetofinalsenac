using Evian.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Evian.Entities
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