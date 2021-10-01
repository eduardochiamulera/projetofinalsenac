using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class ContasReceberDoDiaDTO
    {
        [JsonProperty("tipo")]
        public string Dia { get; set; }

        [JsonProperty("total")]
        public decimal? Total { get; set; }
    }
}
