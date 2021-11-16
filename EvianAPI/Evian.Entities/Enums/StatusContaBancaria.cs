using Evian.Helpers;

namespace Evian.Entities.Enums
{
    public enum StatusContaBancaria
    {
        [Subtitle("EmAberto", "Em aberto", "ABER", "totvs-blue")]
        EmAberto = 0,

        [Subtitle("Pago", "Pago", "PAGO", "green")]
        Pago = 1,

        [Subtitle("BaixadoParcialmente", "Baixado Parcialmente", "BPAR", "gray")]
        BaixadoParcialmente = 2
    }
}
