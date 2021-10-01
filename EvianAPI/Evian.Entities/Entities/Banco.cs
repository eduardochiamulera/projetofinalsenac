using Evian.Entities.Entities.Base;
using Newtonsoft.Json;

namespace Evian.Entities.Entities
{
    public class Banco : DomainBase
    {
        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}
