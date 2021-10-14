using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class CategoriaDTO : BaseDTO
    {
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("categoriaPaiId")]
        public Guid? CategoriaPaiId { get; set; }

        [JsonProperty("tipoCarteira")]
        public string TipoCarteira { get; set; }

        [JsonIgnore]
        public int Level => CategoriaPaiId == null ? 0 : 1;

        [JsonProperty("categoriaPai")]
        public CategoriaDTO CategoriaPai { get; set; }
    }
}