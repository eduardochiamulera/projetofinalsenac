using Evian.Entities.Base;
using System;

namespace Evian.Entities
{
    public class ContaFinanceiraBaixa : EmpresaBase
    {
        public DateTime Data { get; set; }

        public Guid? ContaPagarId { get; set; }

        public Guid? ContaReceberId { get; set; }

        public Guid ContaBancariaId { get; set; }

        public decimal Valor { get; set; }

        public string Observacao { get; set; }

        public virtual ContaPagar ContaPagar { get; set; }

        public virtual ContaReceber ContaReceber { get; set; }

        public virtual ContaBancaria ContaBancaria { get; set; }
    }
}