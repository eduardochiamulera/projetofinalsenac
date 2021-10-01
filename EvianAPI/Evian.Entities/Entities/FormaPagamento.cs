using Evian.Entities.Entities.Base;
using Evian.Entities.Enums;
using Evian.Helpers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.Entities
{
    public class FormaPagamento : EmpresaBase
    {
        public string Descricao { get; set; }

        [JsonIgnore]
        public TipoFormaPagamento TipoFormaPagamento { get; set; }

        [NotMapped]
        [JsonProperty("tipoFormaPagamentoValue")]
        public string TipoFormaPagamentoValue
        {
            get
            {
                return EnumHelper.GetValue(typeof(TipoFormaPagamento), TipoFormaPagamento.ToString());
            }
        }

        [NotMapped]
        [JsonProperty("tipoFormaPagamento")]
        public string TipoFormaPagamentoRest
        {
            get { return ((int)TipoFormaPagamento).ToString(); }
            set { TipoFormaPagamento = (TipoFormaPagamento)System.Enum.Parse(typeof(TipoFormaPagamento), value); }
        }
    }
}
