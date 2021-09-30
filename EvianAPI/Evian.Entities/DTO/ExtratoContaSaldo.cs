using Newtonsoft.Json;
using System;

namespace Evian.Entities.DTO
{
    public class ExtratoContaSaldo
    {
        [JsonProperty("contaBancariaId")]
        public Guid ContaBancariaId { get; set; }

        [JsonProperty("contaBancariaDescricao")]
        public string ContaBancariaDescricao { get; set; }

        [JsonProperty("saldoConsolidado")]
        public decimal SaldoConsolidado { get; set; }
    }
}