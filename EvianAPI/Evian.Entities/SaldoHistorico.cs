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

        public double SaldoDia { get; set; }

        public double SaldoConsolidado { get; set; }

        public double TotalRecebimentos { get; set; }

        public double TotalPagamentos { get; set; }

        public virtual ContaBancaria ContaBancaria { get; set; }
    }
}