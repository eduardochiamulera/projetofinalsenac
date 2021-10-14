using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class CidadeDTO : BaseDTO
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("codigoIbge")]
        public string CodigoIbge { get; set; }

        [JsonProperty("estadoId")]
        public Guid EstadoId { get; set; }

        [JsonProperty("estado")]
        public virtual EstadoDTO Estado { get; set; }
    }
}