using Newtonsoft.Json;

namespace Evian.Entities.DTO
{
    public class FluxoCaixaSaldo
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