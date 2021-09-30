using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.DTO
{
    public class CondicaoParcelamentoSimulacao
    {
        [NotMapped]
        [JsonProperty("valorReferencia")]
        public double ValorReferencia { get; set; }

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