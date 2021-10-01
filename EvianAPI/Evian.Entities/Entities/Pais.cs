using Evian.Entities.Entities.Base;

namespace Evian.Entities.Entities
{
    public class Pais : DomainBase
    {
        public string Nome { get; set; }
        public string CodigoIbge { get; set; }
        public string Sigla { get; set; }
    }
}