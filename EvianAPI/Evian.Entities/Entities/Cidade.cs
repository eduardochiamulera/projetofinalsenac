using Evian.Entities.Entities.Base;
using System;

namespace Evian.Entities.Entities
{
    public class Cidade : DomainBase
    {
        public string Nome { get; set; }

        public string CodigoIbge { get; set; }

        public Guid EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
    }
}
