using Evian.Entities.Entities.Base;
using System;

namespace Evian.Entities.Entities
{
    public class SaldoHistorico : EmpresaBase
    {
        public DateTime Data { get; set; }

        public Guid ContaBancariaId { get; set; }

        public decimal SaldoDia { get; set; }

        public decimal SaldoConsolidado { get; set; }

        public decimal TotalRecebimentos { get; set; }

        public decimal TotalPagamentos { get; set; }

        public virtual ContaBancaria ContaBancaria { get; set; }
    }
}