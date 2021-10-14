using Newtonsoft.Json;
using System.Collections.Generic;

namespace Evian.Entities.Entities.DTO
{
    public class ResponseSimulacaoDTO
    {
        [JsonProperty("value")]
        public List<CondicaoParcelamentoParcelaDTO> Items { get; set; }
    }
}