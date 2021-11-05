using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class EstadoDTO : BaseDTO
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}
