using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class ContasPagarDoDiaDTO
    {
        [JsonProperty("vencimento")]
        public DateTime Vencimento { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
