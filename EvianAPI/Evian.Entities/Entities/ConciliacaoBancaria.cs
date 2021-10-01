using Evian.Entities.Entities.Base;
using System;
using System.Collections.Generic;

namespace Evian.Entities.Entities
{
    public class ConciliacaoBancaria : EmpresaBase
    {
        public Guid ContaBancariaId { get; set; }

        public string Arquivo { get; set; }

        public virtual ContaBancaria ContaBancaria { get; set; }
        public virtual List<ConciliacaoBancariaItem> ConciliacaoBancariaItens { get; set; }
    }
}