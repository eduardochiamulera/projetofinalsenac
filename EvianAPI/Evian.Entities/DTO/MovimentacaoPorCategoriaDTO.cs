using Evian.Helpers;
using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class MovimentacaoPorCategoriaDTO : BaseDTO
    {
        [JsonProperty("categoriaId")]
        public Guid CategoriaId { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        [JsonProperty("categoriaPaiId")]
        public Guid? CategoriaPaiId { get; set; }

        [JsonProperty("previsto")]
        public double Previsto { get; set; }

        [JsonProperty("realizado")]
        public double? Realizado { get; set; }

        [JsonProperty("soma")]
        public double Soma { get; set; }

        [JsonProperty("tipoCarteira")]
        [APIEnum("TipoCarteira")]
        public string TipoCarteira { get; set; }
    }
}