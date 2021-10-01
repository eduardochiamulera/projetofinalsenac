using Evian.Entities.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Evian.Entities.Entities
{
    public class CondicaoParcelamento : EmpresaBase
    {
        [Required]
        [JsonProperty("descricao")]
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }

        [JsonProperty("qtdParcelas")]
        public int? QtdParcelas { get; set; }

        [JsonProperty("condicoesParcelamento")]
        public string CondicoesParcelamento { get; set; }
    }
}
