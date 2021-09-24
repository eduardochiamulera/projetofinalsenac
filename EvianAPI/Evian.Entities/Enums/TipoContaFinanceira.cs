using Evian.Helpers;

namespace Evian.Entities.Enums
{
    public enum TipoContaFinanceira
    {
        [Subtitle("ContaPagar", "Conta Pagar")]
        ContaPagar = 1,

        [Subtitle("ContaReceber", "Conta Receber")]
        ContaReceber = 2,
    }
}