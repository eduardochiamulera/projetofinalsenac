using Evian.Entities.Entities.DTO;
using Evian.Entities.Entities.Enums;
using Evian.Repository.Core;
using EvianBL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace EvianBL
{
    public class FluxoCaixaBL : GenericDomainBaseBL<FluxoCaixaDTO>
    {
        private readonly UnitOfWork _unitOfWork;

        public FluxoCaixaBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context) 
        {
            _unitOfWork = unitOfWork;
        }

        #region #1 Saldo de Todas as Contas (Consolidado) + AReceber e APagar (hoje)
        public FluxoCaixaSaldoDTO GetSaldos(DateTime dataFinal)
        {
            var saldoTodasAsContas = _unitOfWork.SaldoHistoricoBL.GetSaldos().FirstOrDefault(x => x.ContaBancariaId == Guid.Empty).SaldoConsolidado;
            var dataBase = dataFinal;

            var contasFinanceirasBase = _unitOfWork.ContaFinanceiraBL.All
                .Where(x => x.DataVencimento <= dataBase)
                .Where(x => x.StatusContaBancaria == StatusContaBancaria.EmAberto || x.StatusContaBancaria == StatusContaBancaria.BaixadoParcialmente)
                .Select(item => new
                {
                    ValorPago = item.ValorPago == null ? default(decimal) : (decimal)item.ValorPago,
                    item.TipoContaFinanceira,
                    item.ValorPrevisto
                });

            var totalAReceber = contasFinanceirasBase
                .Where(x => x.TipoContaFinanceira == TipoContaFinanceira.ContaReceber)
                .AsNoTracking()
                .ToList()
                .Sum(x => x.ValorPrevisto - x.ValorPago);

            var totalAPagar = contasFinanceirasBase
                .Where(x => x.TipoContaFinanceira == TipoContaFinanceira.ContaPagar)
                .AsNoTracking()
                .ToList()
                .Sum(x => x.ValorPrevisto - x.ValorPago);

            var saldoProjetado = totalAReceber + (totalAPagar * -1) + saldoTodasAsContas;

            return new FluxoCaixaSaldoDTO()
            {
                SaldoAtual = Math.Round(saldoTodasAsContas, 2),
                TotalRecebimentos = Math.Round(totalAReceber, 2),
                TotalPagamentos = Math.Round(totalAPagar, 2),
                SaldoProjetado = Math.Round(saldoProjetado, 2)
            };
        }
        #endregion

        #region #2 Projeção do Fluxo de Caixa
        public List<FluxoCaixaProjecaoDTO> GetProjecao(DateTime dataInicial, DateTime dataFinal, DateGroupType groupType)
        {
            var saldoInicial = _unitOfWork.SaldoHistoricoBL.GetSaldos().FirstOrDefault(x => x.ContaBancariaId == Guid.Empty).SaldoConsolidado;
            var dataInicialVencidas = dataInicial.AddDays(-1);

            var contasFinanceirasVencidas = _unitOfWork.ContaFinanceiraBL.All
                .Where(x => x.StatusContaBancaria == StatusContaBancaria.EmAberto || x.StatusContaBancaria == StatusContaBancaria.BaixadoParcialmente)
                .Where(x => x.DataVencimento < dataInicial)
                .Select(item => new
                {
                    Data = dataInicialVencidas,
                    ValorPago = item.ValorPago == null ? default(decimal) : (decimal)item.ValorPago,
                    item.Id,
                    item.TipoContaFinanceira,
                    item.ValorPrevisto
                }).ToList();

            var contasFinanceirasPeriodo = _unitOfWork.ContaFinanceiraBL.All
                .Where(x => x.StatusContaBancaria == StatusContaBancaria.EmAberto || x.StatusContaBancaria == StatusContaBancaria.BaixadoParcialmente)
                .Where(x => x.DataVencimento >= dataInicial && x.DataVencimento <= dataFinal)
                .Select(item => new
                {
                    Data = item.DataVencimento,
                    ValorPago = item.ValorPago == null ? default(decimal) : (decimal)item.ValorPago,
                    item.Id,
                    item.TipoContaFinanceira,
                    item.ValorPrevisto
                }).ToList();

            var allContasFinanceiras = contasFinanceirasVencidas.Union(contasFinanceirasPeriodo)
                .OrderBy(x => x.Data).ThenBy(n => n.Id);

            var projecaoFluxoCaixa = (from cc in allContasFinanceiras
                                      group cc by cc.Data into g
                                      select new FluxoCaixaProjecaoDTO
                                      {
                                          Label = g.Key.ToString("dd/MM/yyyy"),
                                          Data = g.Key,
                                          SaldoFinal = default(decimal), // (cumulativo: Calculado abaixo a partir do aggregator)
                                          TotalPagamentos = Math.Round(g.Where(x => x.TipoContaFinanceira == TipoContaFinanceira.ContaPagar).Sum(x => x.ValorPrevisto - x.ValorPago), 2),
                                          TotalRecebimentos = Math.Round(g.Where(x => x.TipoContaFinanceira == TipoContaFinanceira.ContaReceber).Sum(x => x.ValorPrevisto - x.ValorPago), 2)
                                      }).OrderBy(x => x.Data).ToList();

            // Calcula saldos cumulativos (por data):
            // Exemplo:
            // var allSaldos = new List<int> { 1, 3, 12, 19, 33 };
            // var aggregator =  { 1, 1+3, 4+12, 16+19, 35+33 }
            // var aggregator =  { 1, 4, 16, 35, 68 }

            //var projecaoGroupped = projecaoFluxoCaixa;
            projecaoFluxoCaixa = ResolveGroup(projecaoFluxoCaixa, groupType);

            var aggregator = new AggregatorSaldosDTO { SumSaldoConsolidado = saldoInicial };
            var aggregatorResult = projecaoFluxoCaixa.Aggregate(aggregator, (output, item) =>
            {
                output.SumSaldoConsolidado += (item.TotalRecebimentos - item.TotalPagamentos);
                output.SaldoConsolidado.Add(output.SumSaldoConsolidado);

                return output;
            });

            for (int i = 0; i <= projecaoFluxoCaixa.Count - 1; i++)
                projecaoFluxoCaixa[i].SaldoFinal = Math.Round(aggregatorResult.SaldoConsolidado[i], 2);

            return projecaoFluxoCaixa;
        }

        private string GetMonthName(int month)
        {
            string monthName = new CultureInfo("pt-BR").DateTimeFormat.GetAbbreviatedMonthName(month);
            return char.ToUpper(monthName[0]) + monthName.Substring(1);
        }

        private List<FluxoCaixaProjecaoDTO> ResolveGroup(List<FluxoCaixaProjecaoDTO> items, DateGroupType groupType)
        {
            switch (groupType)
            {
                case DateGroupType.Day:
                default:
                    return items;
                //case DateGroupType.Week:
                //return items.GroupBy(cc => new { month = cc.Data.Month, year = cc.Data.Year })
                //    .Select(g => new FluxoCaixaProjecao()
                //    {
                //        Label = $"{g.Key.month}/{g.Key.year}",
                //        TotalPagamentos = Math.Round(g.Sum(x => x.TotalPagamentos)),
                //        TotalRecebimentos = Math.Round(g.Sum(x => x.TotalRecebimentos), 2)
                //    }).ToList();
                case DateGroupType.Month:
                    return items.GroupBy(cc => new { cc.Data.Month, cc.Data.Year })
                        .Select(g => new FluxoCaixaProjecaoDTO
                        {
                            Label = $"{GetMonthName(g.Key.Month)}/{g.Key.Year}",
                            TotalPagamentos = Math.Round(g.Sum(x => x.TotalPagamentos), 2),
                            TotalRecebimentos = Math.Round(g.Sum(x => x.TotalRecebimentos), 2)
                        }).ToList();
                case DateGroupType.Quarter:
                    return items.GroupBy(cc => new { Quarter = (cc.Data.Month - 1) / 3, cc.Data.Year })
                        .Select(g => new FluxoCaixaProjecaoDTO
                        {
                            Label = $"T{g.Key.Quarter + 1}/{g.Key.Year}",
                            TotalPagamentos = Math.Round(g.Sum(x => x.TotalPagamentos), 2),
                            TotalRecebimentos = Math.Round(g.Sum(x => x.TotalRecebimentos), 2)
                        }).ToList();
                case DateGroupType.Halfyear:
                    return items.GroupBy(cc => new { Halfyear = (cc.Data.Month - 1) / 6, cc.Data.Year })
                        .Select(g => new FluxoCaixaProjecaoDTO
                        {
                            Label = $"S{g.Key.Halfyear + 1}/{g.Key.Year}",
                            TotalPagamentos = Math.Round(g.Sum(x => x.TotalPagamentos), 2),
                            TotalRecebimentos = Math.Round(g.Sum(x => x.TotalRecebimentos), 2)
                        }).ToList();
                case DateGroupType.Year:
                    return items.GroupBy(cc => new { cc.Data.Year })
                        .Select(g => new FluxoCaixaProjecaoDTO
                        {
                            Label = $"{g.Key.Year}",
                            TotalPagamentos = Math.Round(g.Sum(x => x.TotalPagamentos), 2),
                            TotalRecebimentos = Math.Round(g.Sum(x => x.TotalRecebimentos), 2)
                        }).ToList();
            }
        }

        #endregion
    }
}
