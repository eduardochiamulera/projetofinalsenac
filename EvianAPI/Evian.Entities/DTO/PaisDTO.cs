using Newtonsoft.Json;

namespace Evian.Entities.Entities.DTO
{
    public class PaisDTO : EmpresaBaseDTO
    {
        [JsonProperty("sigla")]
        public string Sigla { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("utcId")]
        public string UtcId { get; set; }

        [JsonProperty("codigoIbge")]
        public string CodigoIbge { get; set; }
    }
}
