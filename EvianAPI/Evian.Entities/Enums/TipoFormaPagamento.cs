﻿using Evian.Helpers;
using System.Xml.Serialization;

namespace Evian.Entities.Enums
{
    public enum TipoFormaPagamento
    {
        [XmlEnum(Name = "01")]
        [Subtitle("Dinheiro", "Dinheiro")]
        Dinheiro = 1,

        [XmlEnum(Name = "02")]
        [Subtitle("Cheque", "Cheque")]
        Cheque = 2,

        [XmlEnum(Name = "03")]
        [Subtitle("CartaoCredito", "Cartão de Crédito")]
        CartaoCredito = 3,

        [XmlEnum(Name = "04")]
        [Subtitle("CartaoDebito", "Cartão de Débito")]
        CartaoDebito = 4,

        [XmlEnum(Name = "05")]
        [Subtitle("CreditoLoja", "Crédito Loja")]
        CreditoLoja = 5,

        [XmlEnum(Name = "06")]
        [Subtitle("Transferencia", "Transferência")]
        Transferencia = 6,

        [XmlEnum(Name = "10")]
        [Subtitle("ValeAlimentacao", "Vale Alimentação")]
        ValeAlimentacao = 10,

        [XmlEnum(Name = "11")]
        [Subtitle("ValeRefeicao", "Vale Refeição")]
        ValeRefeicao = 11,

        [XmlEnum(Name = "12")]
        [Subtitle("ValePresente", "Vale Presente")]
        ValePresente = 12,

        [XmlEnum(Name = "13")]
        [Subtitle("ValeCombustivel", "Vale Combustível")]
        ValeCombustivel = 13,

        [XmlEnum(Name = "14")]
        [Subtitle("DuplicataMercantil", "Duplicata Mercantil")]
        DuplicataMercantil = 14,

        [XmlEnum(Name = "15")]
        [Subtitle("Boleto", "Boleto")]
        Boleto = 15,

        [Subtitle("PIX", "Pix")]
        PIX = 16,

        [XmlEnum(Name = "90")]
        [Subtitle("SemPagamento", "Sem Pagamento")]
        SemPagamento = 90,

        [XmlEnum(Name = "99")]
        [Subtitle("Outros", "Outros")]
        Outros = 99
    }
}
