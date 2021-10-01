using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class DashboardFinanceiroDTO
    {
        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("quantidade")]
        public decimal Quantidade { get; set; }
    }
}
