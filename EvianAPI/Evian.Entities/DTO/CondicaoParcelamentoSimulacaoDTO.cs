using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.Entities.DTO
{
    public class CondicaoParcelamentoSimulacaoDTO
    {
        [NotMapped]
        [JsonProperty("valorReferencia")]
        public decimal ValorReferencia { get; set; }

        [NotMapped]
        [JsonProperty("dataReferencia")]
        [Column(TypeName = "date")]
        public DateTime DataReferencia { get; set; }

        [NotMapped]
        [JsonProperty("qtdParcelas")]
        public int QtdParcelas { get; set; }

        [NotMapped]
        [JsonProperty("condicoesParcelamento")]
        public string CondicoesParcelamento { get; set; }
    }
}