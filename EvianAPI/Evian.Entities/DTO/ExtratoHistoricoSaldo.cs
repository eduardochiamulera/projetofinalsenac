using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Evian.Entities.DTO
{
    public class AggregatorSaldos
    {
        public decimal SumSaldoConsolidado { get; set; }

        public List<decimal> SaldoConsolidado { get; set; }

        public AggregatorSaldos()
        {
            SaldoConsolidado = new List<decimal>();

            SumSaldoConsolidado = default(decimal);
        }
    }

    public class ExtratoHistoricoSaldo
    {
        [JsonProperty("contaBancariaId")]
        public Guid ContaBancariaId { get; set; }

        [JsonProperty("contaBancariaDescricao")]
        public string ContaBancariaDescricao { get; set; }

        [JsonProperty("saldos")]
        public List<ExtratoSaldoHistoricoItem> Saldos { get; set; }

        public ExtratoHistoricoSaldo()
        {
            this.Saldos = new List<ExtratoSaldoHistoricoItem>();
        }
    }

    public class ExtratoSaldoHistoricoItem
    {
        [JsonProperty("data")]
        public DateTime Data { get; set; }

        [JsonProperty("totalPagamentos")]
        public decimal TotalPagamentos { get; set; }

        [JsonProperty("totalRecebimentos")]
        public decimal TotalRecebimentos { get; set; }

        [JsonProperty("saldoDia")]
        public decimal SaldoDia { get; set; }

        [JsonProperty("saldoConsolidado")]
        public decimal SaldoConsolidado { get; set; }

    }
}