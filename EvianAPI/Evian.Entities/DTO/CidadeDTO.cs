using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class CidadeDTO : BaseDTO
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}