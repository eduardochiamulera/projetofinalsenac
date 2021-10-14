using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class RelatorioCentroCustoCPCRDTO : BaseDTO
    {
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("observacao")]
        public string Observacao { get; set; }

        [JsonProperty("clienteId")]
        public Guid ClienteId { get; set; }

        [JsonProperty("formaPagamentoId")]
        public Guid? FormaPagamentoId { get; set; }

        [JsonProperty("dataEmissaoInicial")]
        public DateTime? DataEmissaoInicial { get; set; }

        [JsonProperty("dataEmissaoFinal")]
        public DateTime? DataEmissaoFinal { get; set; }

        [JsonProperty("dataInicial")]
        public DateTime? DataInicial { get; set; }

        [JsonProperty("dataFinal")]
        public DateTime? DataFinal { get; set; }

        [JsonProperty("condicaoParcelamentoId")]
        public Guid? CondicaoParcelamentoId { get; set; }

        [JsonProperty("categoriaFinanceiraId")]
        public Guid? CategoriaFinanceiraId { get; set; }

        [JsonProperty("valor")]
        public double? Valor { get; set; }

        [JsonProperty("Numero")]
        public int? Numero { get; set; }

    }
}