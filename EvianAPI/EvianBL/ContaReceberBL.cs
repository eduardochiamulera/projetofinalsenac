﻿using Evian.Entities;
using Evian.Entities.DTO;
using Evian.Entities.Enums;
using Evian.Helpers;
using Evian.Notifications;
using Evian.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvianBL
{
    public class ContaReceberBL : EmpresaBL<ContaReceber>
    {

        public ContaReceberBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void Insert(ContaReceber entity)
        {
            entity.EmpresaId = EmpresaId;
            entity.UsuarioInclusao = AppUser;

            var repetir = RepeticaoValida(entity);

            entity.ValorPrevisto = Math.Round(entity.ValorPrevisto, 2);
            entity.ValorPago = entity.ValorPago.HasValue ? Math.Round(entity.ValorPago.Value, 2) : entity.ValorPago;

            //na nova Transação e quando status nao definido
            if (entity.StatusContaBancaria == default(StatusContaBancaria))
                entity.StatusContaBancaria = StatusContaBancaria.EmAberto;

            var condicoesParcelamento = _unitOfWork.CondicaoParcelamentoBL.GetPrestacoes(entity.CondicaoParcelamentoId, entity.DataVencimento, entity.ValorPrevisto);
            var contaFinanceiraPrincipal = entity.Id == default(Guid) ? Guid.NewGuid() : entity.Id;

            GravaParcelamentoRepeticoes(entity, repetir, condicoesParcelamento, contaFinanceiraPrincipal);
        }

        private void GravaParcelamentoRepeticoes(ContaReceber entity, bool repetir, List<CondicaoParcelamentoParcela> condicoesParcelamento, Guid contaFinanceiraPrincipal)
        {
            var numero = default(int);
            if (All.Any())
            {
                numero = All.Max(x => x.Numero);
            }

            for (int iParcela = 0; iParcela < condicoesParcelamento.Count; iParcela++)
            {
                numero += 1;
                var parcela = condicoesParcelamento[iParcela];
                var itemContaReceber = new ContaReceber();
                entity.CopyProperties<ContaReceber>(itemContaReceber);

                GravaParcelamento(entity, repetir, contaFinanceiraPrincipal, iParcela, parcela, itemContaReceber, numero);

                if (repetir)
                {
                    //GravaRepeticoes(entity, contaFinanceiraPrincipal, itemContaReceber);
                }
            }
        }

        private void GravaParcelamento(ContaReceber entity, bool repetir, Guid contaFinanceiraPrincipal, int iParcela, CondicaoParcelamentoParcela parcela, ContaReceber itemContaReceber, int numero)
        {
            itemContaReceber.Notification.Errors.AddRange(entity.Notification.Errors);
            itemContaReceber.DataVencimento = parcela.DataVencimento;
            itemContaReceber.DescricaoParcela = parcela.DescricaoParcela;
            itemContaReceber.ValorPrevisto = parcela.Valor;
            itemContaReceber.ValorPago = entity.StatusContaBancaria == StatusContaBancaria.Pago ? parcela.Valor : entity.ValorPago;

            if (iParcela == default(int))
                itemContaReceber.Id = contaFinanceiraPrincipal;
            else
            {
                itemContaReceber.Id = Guid.NewGuid();
                itemContaReceber.ContaReceberParcelaPaiId = contaFinanceiraPrincipal;

                if (repetir)
                    itemContaReceber.ContaReceberParcelaPaiId = contaFinanceiraPrincipal;
            }

            itemContaReceber.Numero = numero;

            base.Insert(itemContaReceber);

            if (entity.StatusContaBancaria == StatusContaBancaria.Pago || entity.StatusContaBancaria == StatusContaBancaria.BaixadoParcialmente) { }
                //contaFinanceiraBaixaBL.GeraContaFinanceiraBaixa(itemContaReceber);
        }

        //private void GravaRepeticoes(ContaReceber entity, Guid contaFinanceiraPrincipal, ContaReceber itemContaReceber)
        //{
        //    var numero = default(int);
        //    if (All.Any())
        //    {
        //        numero = All.Max(x => x.Numero);
        //    }

        //    for (int iRepeticao = 1; iRepeticao <= entity.NumeroRepeticoes; iRepeticao++)
        //    {
        //        numero += 1;
        //        var itemContaReceberRepeticao = new ContaReceber();
        //        itemContaReceber.CopyProperties<ContaReceber>(itemContaReceberRepeticao);
        //        itemContaReceberRepeticao.ContaFinanceiraParcelaPaiId = null;
        //        itemContaReceberRepeticao.Notification.Errors.AddRange(itemContaReceber.Notification.Errors);
        //        itemContaReceberRepeticao.Id = default(Guid);
        //        itemContaReceberRepeticao.ContaFinanceiraRepeticaoPaiId = contaFinanceiraPrincipal;

        //        switch (entity.TipoPeriodicidade)
        //        {
        //            case TipoPeriodicidade.Semanal:
        //                itemContaReceberRepeticao.DataVencimento = itemContaReceberRepeticao.DataVencimento.AddDays(iRepeticao * 7);
        //                break;
        //            case TipoPeriodicidade.Mensal:
        //                itemContaReceberRepeticao.DataVencimento = itemContaReceberRepeticao.DataVencimento.AddMonths(iRepeticao);
        //                break;
        //            case TipoPeriodicidade.Anual:
        //                itemContaReceberRepeticao.DataVencimento = itemContaReceberRepeticao.DataVencimento.AddYears(iRepeticao);
        //                break;
        //        }
        //        itemContaReceberRepeticao.DataEmissao = itemContaReceberRepeticao.DataVencimento;
        //        itemContaReceberRepeticao.Numero = numero;

        //        base.Insert(itemContaReceberRepeticao);

        //        if (entity.StatusContaBancaria == StatusContaBancaria.Pago || entity.StatusContaBancaria == StatusContaBancaria.BaixadoParcialmente)
        //            contaFinanceiraBaixaBL.GeraContaFinanceiraBaixa(itemContaReceberRepeticao);
        //    }
        //}

        public override void Update(ContaReceber entity)
        {
            var contaReceberDb = All.AsNoTracking().FirstOrDefault(x => x.Id == entity.Id);

            entity.Fail(contaReceberDb.CondicaoParcelamentoId != entity.CondicaoParcelamentoId, AlteracaoCondicaoParcelamento);
            entity.Fail((contaReceberDb.Repetir != entity.Repetir) || (contaReceberDb.TipoPeriodicidade != entity.TipoPeriodicidade) ||
                (contaReceberDb.NumeroRepeticoes != entity.NumeroRepeticoes), AlteracaoConfiguracaoRecorrencia);

            base.Update(entity);
        }

        public override void Delete(ContaReceber entityToDelete)
        {
            //contaFinanceiraBaixaBL.All.Where(x => x.ContaFinanceiraId == entityToDelete.Id).OrderBy(x => x.DataInclusao).ToList()
            //    .ForEach(itemBaixa => { contaFinanceiraBaixaBL.Delete(itemBaixa); });

            base.Delete(entityToDelete);
        }

        public List<ContaReceber> GetParcelas(Guid contaFinanceiraParcelaPaiId)
        {
            var parcelas = new List<ContaReceber>();


            if (All.Any(x => x.Id == contaFinanceiraParcelaPaiId))
                parcelas.Add(All.Where(x => x.Id == contaFinanceiraParcelaPaiId).AsNoTracking().FirstOrDefault());
            if (All.Any(x => x.ContaReceberParcelaPaiId == contaFinanceiraParcelaPaiId))
                parcelas.AddRange(All.Where(x => x.ContaReceberParcelaPaiId == contaFinanceiraParcelaPaiId).AsNoTracking().OrderBy(x => x.DataInclusao));

            return parcelas;
        }

        private static bool RepeticaoValida(ContaReceber entity)
        {
            if (entity.Repetir)
            {
                const int limiteSemanal = 208;
                const int limiteMensal = 48;
                const int limiteAnual = 4;

                entity.Fail(entity.TipoPeriodicidade == TipoPeriodicidade.Nenhuma, TipoPeriodicidadeInvalida);
                entity.Fail(!entity.NumeroRepeticoes.HasValue, NumeroRepeticoesInvalido);

                entity.Fail((entity.TipoPeriodicidade != TipoPeriodicidade.Nenhuma && entity.NumeroRepeticoes.HasValue) &&
                            ((entity.TipoPeriodicidade == TipoPeriodicidade.Semanal && !(entity.NumeroRepeticoes.Value > 0 && entity.NumeroRepeticoes.Value <= limiteSemanal)) ||
                             (entity.TipoPeriodicidade == TipoPeriodicidade.Mensal && !(entity.NumeroRepeticoes.Value > 0 && entity.NumeroRepeticoes.Value <= limiteMensal)) ||
                             (entity.TipoPeriodicidade == TipoPeriodicidade.Anual && !(entity.NumeroRepeticoes.Value > 0 && entity.NumeroRepeticoes.Value <= limiteAnual)))
                    , RepeticoesInvalidas);

                return entity.Repetir && entity.IsValid();
            }

            return false;
        }

        //public List<ContaFinanceiraPorStatusVM> GetSaldoStatus(DateTime dataFinal, DateTime dataInicial)
        //{
        //    List<ContaFinanceiraPorStatusVM> listaResult = new List<ContaFinanceiraPorStatusVM>();
        //    int QtdTotal = All.AsNoTracking().Where(x => x.DataVencimento >= dataInicial && x.DataVencimento <= dataFinal).Count();

        //    var result = All.AsNoTracking().Where(x => x.DataVencimento >= dataInicial && x.DataVencimento <= dataFinal)
        //                                    .Select(x => new { x.ValorPrevisto, x.StatusContaBancaria, x.ValorPago })
        //                                    .GroupBy(x => new { x.StatusContaBancaria })
        //                                    .Select(x => new
        //                                    {
        //                                        x.Key.StatusContaBancaria,
        //                                        Quantidade = x.Count(),
        //                                        valorTotal = x.Sum(y => y.ValorPrevisto),
        //                                        valorPago = x.Sum(y => y.ValorPago)
        //                                    })
        //                                    .ToList();

        //    double? resultDiferenca = result.Where(x => x.StatusContaBancaria == StatusContaBancaria.BaixadoParcialmente).Select(x => x.valorTotal - x.valorPago)?.FirstOrDefault() ?? 0;
        //    double? valorPago = result.Where(x => x.StatusContaBancaria == StatusContaBancaria.BaixadoParcialmente).Select(x => x.valorPago)?.FirstOrDefault() ?? 0;

        //    result.ForEach(x =>
        //    {
        //        listaResult.Add(new ContaFinanceiraPorStatusVM
        //        {
        //            Status = EnumHelper.GetValue(typeof(StatusContaBancaria), x.StatusContaBancaria.ToString()),
        //            Quantidade = x.Quantidade,
        //            QuantidadeTotal = QtdTotal,
        //            Valortotal = (x.StatusContaBancaria.ToString() == "EmAberto")
        //                 ? x.valorTotal + (double)resultDiferenca
        //                 : (x.StatusContaBancaria.ToString() == "Pago")
        //                    ? x.valorTotal + (double)valorPago
        //                    : x.valorTotal
        //        });
        //    });

        //    if (!result.Where(x => x.StatusContaBancaria == StatusContaBancaria.EmAberto).Any() && result != null)
        //    {
        //        listaResult.Add(new ContaFinanceiraPorStatusVM()
        //        {
        //            Status = "Em aberto",
        //            Valortotal = (double)resultDiferenca
        //        });
        //    }

        //    return listaResult;
        //}

        public static Error RepeticoesInvalidas = new Error("Número de repetições inválido. Somente até 48 Meses (4 Anos / 208 Semanas).");
        public static Error AlteracaoCondicaoParcelamento = new Error("Não é permitido alterar a condição de parcelamento.");
        public static Error AlteracaoConfiguracaoRecorrencia = new Error("Não é permitido alterar as configurações de recorrência.");
        public static Error TipoPeriodicidadeInvalida = new Error("Periodicidade inválida", "tipoPeriodicidade");
        public static Error NumeroRepeticoesInvalido = new Error("Número de repetições inválido", "numeroRepeticoes");
    }
}