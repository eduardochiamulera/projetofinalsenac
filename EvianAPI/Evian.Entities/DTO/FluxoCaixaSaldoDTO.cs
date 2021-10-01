using Newtonsoft.Json;

namespace Evian.Entities.Entities.DTO
{
    public class FluxoCaixaSaldoDTO
    {
        [JsonProperty("saldoAtual")]
        public decimal SaldoAtual { get; set; }

        [JsonProperty("totalRecebimentos")]
        public decimal TotalRecebimentos { get; set; }

        [JsonProperty("totalPagamentos")]
        public decimal TotalPagamentos { get; set; }

        [JsonProperty("saldoProjetado")]
        public decimal SaldoProjetado { get; set; }
    }
}