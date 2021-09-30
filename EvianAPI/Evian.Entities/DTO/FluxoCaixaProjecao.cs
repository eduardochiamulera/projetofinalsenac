using Newtonsoft.Json;
using System;

namespace Evian.Entities.DTO
{
    public class FluxoCaixaProjecao
    {
        [JsonIgnore]
        public DateTime Data { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("totalRecebimentos")]
        public decimal TotalRecebimentos { get; set; }

        [JsonProperty("totalPagamentos")]
        public decimal TotalPagamentos { get; set; }

        [JsonProperty("saldoFinal")]
        public decimal SaldoFinal { get; set; }
    }
}