
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Evian.Entities.Entities.DTO
{
    public class ConciliacaoBancariaItemDTO : BaseDTO
    {
        [JsonProperty("conciliacaoBancariaId")]
        public Guid ConciliacaoBancariaId { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }

        [JsonProperty("data")]       
        public DateTime Data { get; set; }

        [JsonProperty("ofxLancamentoMD5")]
        public string OfxLancamentoMD5 { get; set; }

        [JsonProperty("statusConciliado")]
        public string StatusConciliado { get; set; }

        #region Navigations Properties
        [JsonProperty("conciliacaoBancariaItemContasFinanceiras")]
        public virtual List<ConciliacaoBancariaItemContaFinanceiraDTO> ConciliacaoBancariaItemContasFinanceiras { get; set; }

        #endregion
    }
}