using Evian.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Evian.Entities
{
    public class Cidade : DomainBase
    {
        public string Nome { get; set; }

        public string CodigoIbge { get; set; }

        public Guid EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
    }
}
