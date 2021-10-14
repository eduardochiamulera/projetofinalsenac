using Newtonsoft.Json;

namespace Evian.Entities.Entities.DTO
{
    public class CondicaoParcelamentoDTO : BaseDTO
    {
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("qtdParcelas")]
        public int? QtdParcelas { get; set; }

        [JsonProperty("condicoesParcelamento")]
        public string CondicoesParcelamento { get; set; }
    }
}
