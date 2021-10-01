using Newtonsoft.Json;

namespace Evian.Entities.Entities.DTO
{
    public class BancoDTO : EmpresaBaseDTO
    {
        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}