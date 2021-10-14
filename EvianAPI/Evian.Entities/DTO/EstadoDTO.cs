using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class EstadoDTO : BaseDTO
    {
        [JsonProperty("sigla")]
        public string Sigla { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("utcId")]
        public string UtcId { get; set; }

        [JsonProperty("codigoIbge")]
        public string CodigoIbge { get; set; }

        [JsonProperty("paisId")]
        public Guid PaisId { get; set; }

        [JsonProperty("pais")]
        public virtual PaisDTO Pais { get; set; }
    }
}
