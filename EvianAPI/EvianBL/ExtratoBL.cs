using Evian.Entities.Entities.DTO;
using Evian.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvianBL
{
    public class ExtratoBL : GenericDomainBaseBL<ExtratoDTO>
    {
        private readonly UnitOfWork _unitOfWork;
        private const string labelTodasAsContas = "Todas as Contas";

        public ExtratoBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context) { }

        #region #1 Saldos Consolidados por Conta
        public List<ExtratoContaSaldoDTO> GetSaldos()
        {
            return _unitOfWork.SaldoHistoricoBL.GetSaldos();
        }
        #endregion

        #region #2 Historico Saldos
        public ExtratoHistoricoSaldoDTO GetHistoricoSaldos(DateTime dataInicial, DateTime dataFinal, Guid? contaBancariaId)
        {
            var dataSaldoInicial = dataInicial.AddDays(-1);
            var saldosIniciais = (from si in _unitOfWork.SaldoHistoricoBL.All
                                  where si.Data < dataInicial.Date && si.ContaBancariaId == (contaBancariaId ?? si.ContaBancariaId)
                                  group si by si.ContaBancariaId into g
                                  let dataRecord = g.OrderByDescending(t => t.Data).FirstOrDefault()
                                  select new
                                  {
                                      ContaBancariaId = g.Key,
                                      Data = dataSaldoInicial,
                                      SaldoConsolidado = dataRecord.SaldoConsolidado,
                                      TotalPagamentos = dataInicial == dataRecord.Data ? dataRecord.TotalPagamentos : default(decimal),
                                      TotalRecebimentos = dataInicial == dataRecord.Data ? dataRecord.TotalRecebimentos : default(decimal),
                                      SaldoDia = dataRecord.SaldoDia
                                  }).ToList();

            var saldoInicial = saldosIniciais.Sum(e => e.SaldoConsolidado);
            var saldosIniciaisAny = saldosIniciais.Any();

            var saldosPeriodo = (from sp in _unitOfWork.SaldoHistoricoBL.All
                                 where sp.Data >= dataInicial && sp.Data <= dataFinal && sp.ContaBancariaId == (contaBancariaId ?? sp.ContaBancariaId)
                                 select new
                                 {
                                     ContaBancariaId = sp.ContaBancariaId,
                                     Data = sp.Data,
                                     SaldoConsolidado = sp.SaldoConsolidado,
                                     TotalPagamentos = sp.TotalPagamentos,
                                     TotalRecebimentos = sp.TotalRecebimentos,
                                     SaldoDia = sp.SaldoDia
                                 }).ToList();

            var listOfBalances = saldosIniciais.Union(saldosPeriodo).Select(itemSaldo => new ExtratoSaldoHistoricoItemDTO()
            {
                Data = itemSaldo.Data,
                SaldoConsolidado = Math.Round(itemSaldo.SaldoConsolidado, 2),
                SaldoDia = Math.Round(itemSaldo.SaldoDia, 2),
                TotalPagamentos = Math.Round(itemSaldo.TotalPagamentos, 2),
                TotalRecebimentos = Math.Round(itemSaldo.TotalRecebimentos, 2)
            }).OrderBy(x => x.Data).ToList();

            if (!listOfBalances.Any(x => x.Data == dataFinal))
            {
                var lastRecord = listOfBalances.LastOrDefault();

                if (lastRecord != null)
                {
                    listOfBalances.Add(new ExtratoSaldoHistoricoItemDTO()
                    {
                        Data = dataFinal,
                        SaldoConsolidado = lastRecord.SaldoConsolidado,
                        SaldoDia = default(decimal),
                        TotalPagamentos = default(decimal),
                        TotalRecebimentos = default(decimal)
                    });
                }
            }

            if (contaBancariaId.HasValue)
                return GetHistoricoSaldosByConta(listOfBalances, (Guid)contaBancariaId);

            return GetHistoricoSaldosByAll(listOfBalances, saldoInicial, saldosIniciaisAny);
        }

        private ExtratoHistoricoSaldoDTO GetHistoricoSaldosByConta(List<ExtratoSaldoHistoricoItemDTO> listOfBalances, Guid contaBancariaId)
        {
            var contaBancaria = _unitOfWork.ContaBancariaBL.All.Where(x => x.Id == contaBancariaId).Select(x => new { x.NomeConta }).FirstOrDefault();

            return new ExtratoHistoricoSaldoDTO()
            {
                ContaBancariaId = contaBancariaId,
                ContaBancariaDescricao = contaBancaria != null
                    ? contaBancaria.NomeConta
                    : string.Empty,
                Saldos = listOfBalances
            };
        }

        private ExtratoHistoricoSaldoDTO GetHistoricoSaldosByAll(List<ExtratoSaldoHistoricoItemDTO> listOfBalances, decimal saldoInicial, bool saldosIniciaisAny = false)
        {
            // Agrupamento por Data
            listOfBalances = (from s in listOfBalances
                              group s by s.Data into g
                              select new ExtratoSaldoHistoricoItemDTO()
                              {
                                  Data = g.Key,
                                  SaldoConsolidado = listOfBalances.FirstOrDefault().SaldoConsolidado, //(cumulativo de todas as contas e calculado com base no saldo do dia)
                                  SaldoDia = Math.Round(g.Sum(k => k.SaldoDia), 2),
                                  TotalPagamentos = Math.Round(g.Sum(k => k.TotalPagamentos), 2),
                                  TotalRecebimentos = Math.Round(g.Sum(k => k.TotalRecebimentos), 2)
                              }).ToList();

            // Calcula saldos cumulativos (por data):
            // Exemplo:
            // var allSaldos = new List<int> { 1, 3, 12, 19, 33 };
            // var aggregator =  { 1, 1+3, 4+12, 16+19, 35+33 }
            // var aggregator =  { 1, 4, 16, 35, 68 }

            var cont = 0;
            var aggregator = new AggregatorSaldosDTO();
            var aggregatorResult = listOfBalances.Aggregate(aggregator, (output, item) =>
            {
                output.SumSaldoConsolidado += item.SaldoDia;

                if (cont < 1)
                {
                    //o saldo inicial pode estar zerado, por não ter datas anteriores ou pois a soma das datas anteriores podem ter se anuladas(zeradas +50 -50)
                    output.SumSaldoConsolidado = (saldoInicial != 0 && saldosIniciaisAny) ? saldoInicial : output.SumSaldoConsolidado;
                }

                output.SaldoConsolidado.Add(output.SumSaldoConsolidado);

                cont++;
                return output;
            });

            for (int i = 0; i <= listOfBalances.Count - 1; i++)
                listOfBalances[i].SaldoConsolidado = Math.Round(aggregatorResult.SaldoConsolidado[i], 2);

            return new ExtratoHistoricoSaldoDTO()
            {
                ContaBancariaId = Guid.Empty,
                ContaBancariaDescricao = labelTodasAsContas,
                Saldos = listOfBalances
            };
        }
        #endregion

        #region #3 Detalhes das Movimentações
        public List<ExtratoDetalheDTO> GetExtratoDetalheDTO(DateTime dataInicial, DateTime dataFinal, Guid? contaBancariaId, int skipRecords, int takeRecords)
        {
            var contasBancarias = _unitOfWork.ContaBancariaBL.All.Select(x => new { x.Id, x.NomeConta }).ToList();

            var movimentacoes = (from mov in _unitOfWork.MovimentacaoBL.AllIncluding(x => x.ContaFinanceira, x => x.ContaFinanceira.Pessoa, x => x.ContaBancariaDestino, x => x.ContaBancariaOrigem)
                                 where (mov.Data >= dataInicial && mov.Data <= dataFinal) && mov.Ativo &&
                                 //((mov.ContaFinanceira != null && (mov.ContaFinanceira.Ativo && mov.ContaFinanceira.Pessoa.Ativo)) || (mov.ContaFinanceira == null)) &&
                                 (mov.ContaBancariaDestino.Ativo || mov.ContaBancariaOrigem.Ativo) &&
                                 (
                                     (contaBancariaId.HasValue) ?
                                     (mov.ContaBancariaDestinoId == contaBancariaId) || (mov.ContaBancariaOrigemId == contaBancariaId) :
                                     (mov.ContaBancariaDestino == null) || (mov.ContaBancariaOrigemId == null)
                                 )
                                 select new ExtratoDetalheDTO()
                                 {
                                     ContaBancariaId = (Guid)(mov.ContaBancariaDestinoId ?? mov.ContaBancariaOrigemId),
                                     ContaBancariaDescricao = string.Empty,
                                     DataMovimento = mov.Data,
                                     DataInclusao = mov.DataInclusao,
                                     DescricaoLancamento = mov.Descricao == null ? (mov.ContaFinanceira != null ? mov.ContaFinanceira.Descricao : "") : mov.Descricao,
                                     ContaFinanceiraNumero = mov.ContaFinanceira != null ? mov.ContaFinanceira.Numero.ToString() :  "",
                                     PessoaNome = mov.ContaFinanceira != null ? mov.ContaFinanceira.Pessoa.Nome : "",
                                     ValorLancamento = Math.Round(mov.Valor, 2),
                                 }).OrderByDescending(x => x.DataMovimento).ThenByDescending(x => x.DataInclusao).Skip(skipRecords).Take(takeRecords).ToList();

            movimentacoes.ForEach(item =>
                item.ContaBancariaDescricao = contasBancarias.FirstOrDefault(x => x.Id == item.ContaBancariaId).NomeConta ?? item.DescricaoLancamento
            );

            return movimentacoes;
        }

        public int GetExtratoDetalheDTOCount(DateTime dataInicial, DateTime dataFinal, Guid? contaBancariaId)
        {
            var countRecords = _unitOfWork.MovimentacaoBL.AllIncluding(x => x.ContaFinanceira, x => x.ContaFinanceira.Pessoa, x => x.ContaBancariaDestino, x => x.ContaBancariaOrigem)
                .Count(x => x.Data >= dataInicial && x.Data <= dataFinal && x.Ativo &&
                //(x.ContaFinanceira != null && (x.ContaFinanceira.Ativo && x.ContaFinanceira.Pessoa.Ativo) || x.ContaFinanceira == null) &&
                (x.ContaBancariaDestino.Ativo || x.ContaBancariaOrigem.Ativo) &&
                (
                    (contaBancariaId.HasValue)?
                    (x.ContaBancariaDestinoId == contaBancariaId) || (x.ContaBancariaOrigemId == contaBancariaId):
                    (x.ContaBancariaDestino == null) || (x.ContaBancariaOrigem == null)
                )
            );

            return countRecords;
        }

        #endregion
    }
}