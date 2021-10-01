using Fly01.Financeiro.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Evian.Entities.Entities.DTO
{
    public class ConciliacaoBancariaDTO : EmpresaBaseDTO
    {
        [JsonProperty("contaBancariaId")]
        public Guid ContaBancariaId { get; set; }

        [JsonProperty("arquivo")]
        public string Arquivo { get; set; }

        #region Navigations Properties
        [JsonProperty("contaBancaria")]
        public virtual ContaBancariaDTO ContaBancaria { get; set; }

        [JsonProperty("conciliacaoBancariaItens")]
        public virtual List<ConciliacaoBancariaItemDTO> ConciliacaoBancariaItens { get; set; }

        #endregion
    }
}