using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class DashboardContaFinanceiraDTO
    {
        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }
    }
}



