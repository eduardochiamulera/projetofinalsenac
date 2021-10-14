using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    [Serializable]
    public class TransferenciaCadastroDTO : BaseDTO
    {
        [JsonProperty("categoriaDestinoId")]
        public Guid? CategoriaDestinoId { get; set; }

        [JsonProperty("categoriaDestino")]
        public virtual CategoriaDTO CategoriaDestino { get; set; }

        [JsonProperty("descricaoDestino")]
        public string DescricaoDestino { get; set; }
    }
}