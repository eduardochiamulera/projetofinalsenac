using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Evian.Entities.Entities.DTO
{
    public class AggregatorSaldosDTO
    {
        public decimal SumSaldoConsolidado { get; set; }

        public List<decimal> SaldoConsolidado { get; set; }

        public AggregatorSaldosDTO()
        {
            SaldoConsolidado = new List<decimal>();

            SumSaldoConsolidado = default(decimal);
        }
    }

    public class ExtratoHistoricoSaldoDTO
    {
        [JsonProperty("contaBancariaId")]
        public Guid ContaBancariaId { get; set; }

        [JsonProperty("contaBancariaDescricao")]
        public string ContaBancariaDescricao { get; set; }

        [JsonProperty("saldos")]
        public List<ExtratoSaldoHistoricoItemDTO> Saldos { get; set; }

        public ExtratoHistoricoSaldoDTO()
        {
            this.Saldos = new List<ExtratoSaldoHistoricoItemDTO>();
        }
    }

    public class ExtratoSaldoHistoricoItemDTO
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