using Evian.Entities.Entities.Base;
using Evian.Entities.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.Entities
{
    public class Categoria : EmpresaBase
    {
        public string Descricao { get; set; }

        public Guid? CategoriaPaiId { get; set; }

        public TipoCarteira TipoCarteira { get; set; }

        [NotMapped]
        [JsonProperty("tipoCarteira")]
        public string TipoCarteiraRest
        {
            get { return ((int)TipoCarteira).ToString(); }
            set { TipoCarteira = (TipoCarteira)System.Enum.Parse(typeof(TipoCarteira), value); }
        }

        [JsonIgnore]
        public virtual Categoria CategoriaPai { get; set; }
    }
}
