using Evian.Entities.Entities.Base;
using System;

namespace Evian.Entities.Entities
{
    public class ContaBancaria : EmpresaBase
    {
        public string NomeConta { get; set; }

        public string Agencia { get; set; }

        public string DigitoAgencia { get; set; }

        public string Conta { get; set; }

        public string DigitoConta { get; set; }

        public Guid BancoId { get; set; }
        public decimal? ValorInicial { get; set; }

        public virtual Banco Banco { get; set; }

        public virtual SaldoHistorico SaldoHistorico { get; set; }


    }
}