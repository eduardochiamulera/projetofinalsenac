using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class DespesaPorCategoriaDTO : EmpresaBaseDTO
    {
        [JsonProperty("categoriaId")]
        public Guid CategoriaId { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        [JsonProperty("categoriaPaiId")]
        public Guid? CategoriaPaiId { get; set; }

        [JsonProperty("previsto")]
        public decimal Previsto { get; set; }

        [JsonProperty("realizado")]
        public decimal Realizado { get; set; }

        [JsonProperty("soma")]
        public decimal Soma { get; set; }

        [JsonProperty("tipoCarteira")]
        public string TipoCarteira { get; set; }
    }
}