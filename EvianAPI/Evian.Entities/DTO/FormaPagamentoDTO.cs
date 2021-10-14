using Evian.Helpers;
using Newtonsoft.Json;

namespace Evian.Entities.Entities.DTO
{
    public class FormaPagamentoDTO : BaseDTO
    {
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("tipoFormaPagamento")]
        [APIEnum("TipoFormaPagamento")]
        public string TipoFormaPagamento { get; set; }
    }
}
