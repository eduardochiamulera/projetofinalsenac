using Evian.Entities.Entities.Base;
using Evian.Entities.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Evian.Entities.Entities
{
    public class ContaFinanceiraBaixaMultipla : EmpresaBase
    {
        public DateTime Data { get; set; }
        
        public virtual List<Guid> ContasFinanceirasIds { get; set; }
        
        public Guid ContaBancariaId { get; set; }
        
        public string Observacao { get; set; }

        public TipoContaFinanceira TipoContaFinanceira { get; set; }

        public virtual ContaBancaria ContaBancaria { get; set; }
    }
}