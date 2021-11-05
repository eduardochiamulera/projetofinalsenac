using Newtonsoft.Json;

namespace Evian.Entities.Entities.DTO
{
    public class PaisDTO : BaseDTO
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}
