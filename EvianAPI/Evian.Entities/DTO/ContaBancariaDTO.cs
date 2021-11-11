using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    [Serializable]
    public class ContaBancariaDTO : BaseDTO
    {
        [JsonProperty("nomeConta")]
        public string NomeConta { get; set; }

        [JsonProperty("agencia")]
        public string Agencia { get; set; }

        [JsonProperty("digitoAgencia")]
        public string DigitoAgencia { get; set; }

        [JsonProperty("conta")]
        public string Conta { get; set; }

        [JsonProperty("digitoConta")]
        public string DigitoConta { get; set; }

        [JsonProperty("bancoId")]
        public Guid? BancoId { get; set; }

        [JsonProperty("bancoNome")]
        public string BancoNome { get; set; }

        [JsonProperty("banco")]
        public virtual BancoDTO Banco { get; set; }

        [JsonProperty("valorInicial")]
        public decimal? ValorInicial { get; set; }
    }
}