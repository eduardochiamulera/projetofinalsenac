using System;
using Newtonsoft.Json;

namespace Evian.Entities.Entities.DTO
{
    [Serializable]
    public class TransferenciaDTO
    {
        [JsonProperty("movimentacaoOrigem")]
        public MovimentacaoDTO MovimentacaoOrigem { get; set; }

        [JsonProperty("movimentacaoDestino")]
        public MovimentacaoDTO MovimentacaoDestino { get; set; }
    }
}