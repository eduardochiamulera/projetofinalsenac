using Evian.Entities.Base;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Evian.Entities
{
    public class ContaBancaria : EmpresaBase
    {
        public string NomeConta { get; set; }

        public string Agencia { get; set; }

        public string DigitoAgencia { get; set; }

        public string Conta { get; set; }

        public string DigitoConta { get; set; }

        public Guid BancoId { get; set; }

        public virtual Banco Banco { get; set; }

        public decimal? ValorInicial { get; set; }
    }
}