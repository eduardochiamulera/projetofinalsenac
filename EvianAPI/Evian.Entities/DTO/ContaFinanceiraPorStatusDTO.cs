using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evian.Entities.Entities.DTO
{
    public class ContaFinanceiraPorStatusDTO
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("quantidadetotal")]
        public int QuantidadeTotal { get; set; }

        [JsonProperty("valortotal")]
        public decimal Valortotal { get; set; }
    }
}
