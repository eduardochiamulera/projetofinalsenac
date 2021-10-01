using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.Entities.DTO
{
    public class CondicaoParcelamentoParcelaDTO
    {
        public string DescricaoParcela { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DataVencimento { get; set; }

        public decimal Valor { get; set; }
    }
}
