using Evian.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities
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