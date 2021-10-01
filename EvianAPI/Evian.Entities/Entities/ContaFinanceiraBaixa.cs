using Evian.Entities.Entities.Base;
using System;

namespace Evian.Entities.Entities
{
    public class ContaFinanceiraBaixa : EmpresaBase
    {
        public DateTime Data { get; set; }

        public Guid? ContaFinanceiraId { get; set; }

        public Guid ContaBancariaId { get; set; }

        public decimal Valor { get; set; }

        public string Observacao { get; set; }

        public virtual ContaFinanceira ContaFinanceira { get; set; }

        public virtual ContaBancaria ContaBancaria { get; set; }
    }
}