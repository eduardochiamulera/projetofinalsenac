using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class ExtratoContaSaldoDTO
    {
        [JsonProperty("contaBancariaId")]
        public Guid ContaBancariaId { get; set; }

        [JsonProperty("contaBancariaDescricao")]
        public string ContaBancariaDescricao { get; set; }

        [JsonProperty("saldoConsolidado")]
        public decimal SaldoConsolidado { get; set; }
    }
}