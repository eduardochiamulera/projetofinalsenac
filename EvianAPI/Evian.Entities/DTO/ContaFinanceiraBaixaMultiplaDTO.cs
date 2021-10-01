using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evian.Entities.Entities.DTO
{
    [Serializable]
    public class ContaFinanceiraBaixaMultiplaDTO : EmpresaBaseDTO
    {
        [JsonProperty("data")]
        public DateTime Data { get; set; }

        [JsonIgnore]
        public string ContasFinanceirasGuids { get; set; }

        [JsonProperty("contasFinanceirasIds")]
        public List<Guid> ContasFinanceirasIds
        {
            get
            {
                var result = new List<Guid>();
                if (!string.IsNullOrWhiteSpace(ContasFinanceirasGuids))
                {
                    ContasFinanceirasGuids.Split(',').ToList().ForEach(x =>
                    {
                        result.Add(Guid.Parse(x));
                    });
                }
                return result;
            }
        }

        [JsonProperty("contaBancariaId")]
        public Guid ContaBancariaId { get; set; }
        
        [JsonProperty("observacao")]
        public string Observacao { get; set; }

        [JsonProperty("tipoContaFinanceira")]
        public string TipoContaFinanceira { get; set; }

        [JsonProperty("contaBancaria")]
        public virtual ContaBancariaDTO ContaBancaria { get; set; }
    }
}