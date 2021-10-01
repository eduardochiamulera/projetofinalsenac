using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using Evian.Entities.Entities.Enums;
using Evian.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvianBL
{
    public class SaldoHistoricoBL : EmpresaBL<SaldoHistorico>
    {
        private const string labelTodasAsContas = "Todas as Contas";

        public SaldoHistoricoBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public List<ExtratoContaSaldoDTO> GetSaldos()
        {
            var saldos = (from si in AllIncluding(x => x.ContaBancaria)
                          where si.Ativo && si.ContaBancaria.Ativo
                          group si by si.ContaBancariaId into g
                          let dataRecord = g.OrderByDescending(t => t.Data).FirstOrDefault()
                          select new ExtratoContaSaldoDTO()
                          {
                              ContaBancariaDescricao = dataRecord.ContaBancaria.NomeConta,
                              ContaBancariaId = dataRecord.ContaBancariaId,
                              SaldoConsolidado = Math.Round(dataRecord.SaldoConsolidado, 2)
                          }).ToList();

            saldos.Insert(0, new ExtratoContaSaldoDTO()
            {
                ContaBancariaId = Guid.Empty,
                ContaBancariaDescricao = labelTodasAsContas,
                SaldoConsolidado = saldos.Sum(x => x.SaldoConsolidado)
            });

            return saldos;
        }

        public void InsereSaldoInicial(Guid contaBancariaId, decimal? valorInicial = 0m)
        {
            if (contaBancariaId == default(Guid))
                throw new Exception("Conta Bancária Inválida");

            if (!All.Any(x => x.ContaBancariaId == contaBancariaId))
            {
                var saldoInicial = new SaldoHistorico()
                {
                    ContaBancariaId = contaBancariaId,
                    Data = DateTime.Now.Date,
                    SaldoDia = (decimal)valorInicial,
                    SaldoConsolidado = (decimal)valorInicial,
                    TotalPagamentos = default(decimal),
                    TotalRecebimentos = default(decimal),
                };

                base.Insert(saldoInicial);
            }
        }

        public void AtualizaSaldoHistorico(DateTime data, decimal valorBase, Guid contaBancariaId, TipoContaFinanceira tipoContaFinanceira)
        {
            if (contaBancariaId == default(Guid))
                throw new Exception("Conta Bancária Inválida");

            var valorContabil = valorBase;
            if (tipoContaFinanceira == TipoContaFinanceira.ContaPagar)
                valorContabil = valorBase * -1;

            var saldoHistorico = All.FirstOrDefault(x => x.ContaBancariaId == contaBancariaId && x.Data == data.Date);
            if (saldoHistorico == null)
            {
                var ultimoSaldo = All.OrderByDescending(x => x.Data).FirstOrDefault(x => x.ContaBancariaId == contaBancariaId && x.Data < data);

                var saldoConsolidado = default(decimal);
                if (ultimoSaldo != null)
                    saldoConsolidado = ultimoSaldo.SaldoConsolidado;

                saldoConsolidado += valorContabil;

                // Inclui Novo Saldo
                base.Insert(new SaldoHistorico()
                {
                    ContaBancariaId = contaBancariaId,
                    Data = data,
                    SaldoDia = valorContabil,
                    SaldoConsolidado = saldoConsolidado,
                    TotalPagamentos = tipoContaFinanceira == TipoContaFinanceira.ContaPagar ? valorBase : default(decimal),
                    TotalRecebimentos = tipoContaFinanceira == TipoContaFinanceira.ContaReceber ? valorBase : default(decimal)
                });
            }
            else
            {
                saldoHistorico.SaldoConsolidado += valorContabil;

                // Atualiza Saldo Existente
                switch (tipoContaFinanceira)
                {
                    case TipoContaFinanceira.ContaPagar:
                        saldoHistorico.TotalPagamentos += valorBase;
                        break;
                    case TipoContaFinanceira.ContaReceber:
                        saldoHistorico.TotalRecebimentos += valorBase;
                        break;
                }

                saldoHistorico.SaldoDia += valorContabil;

                Update(saldoHistorico);
            }
        }

        public void AtualizaSaldoHistorico(DateTime data, decimal valorBase, Guid contaBancariaId, TipoCarteira tipoCarteira)
        {
            if (contaBancariaId == default(Guid))
                throw new Exception("Conta Bancária Inválida");

            var valorContabil = valorBase;
            if (tipoCarteira == TipoCarteira.Despesa)
                valorContabil *= -1;

            SaldoHistorico saldoHistorico = All.FirstOrDefault(x => x.ContaBancariaId == contaBancariaId && x.Data == data.Date);
            if (saldoHistorico == null)
            {
                var ultimoSaldo = All.OrderByDescending(x => x.Data).FirstOrDefault(x => x.ContaBancariaId == contaBancariaId && x.Data < data);

                var saldoConsolidado = default(decimal);
                if (ultimoSaldo != null)
                    saldoConsolidado = ultimoSaldo.SaldoConsolidado;

                saldoConsolidado += valorContabil;

                // Inclui Novo Saldo
                var sal = new SaldoHistorico()
                {
                    ContaBancariaId = contaBancariaId,
                    Data = data,
                    SaldoDia = valorContabil,
                    SaldoConsolidado = saldoConsolidado,
                    TotalPagamentos = tipoCarteira == TipoCarteira.Despesa ? valorBase : default(decimal),
                    TotalRecebimentos = tipoCarteira == TipoCarteira.Receita ? valorBase : default(decimal)
                };
                base.Insert(sal);
            }
            else
            {
                saldoHistorico.SaldoConsolidado += valorContabil;

                // Atualiza Saldo Existente
                switch (tipoCarteira)
                {
                    case TipoCarteira.Despesa:
                        saldoHistorico.TotalPagamentos += valorBase;
                        break;
                    case TipoCarteira.Receita:
                        saldoHistorico.TotalRecebimentos += valorBase;
                        break;
                }

                saldoHistorico.SaldoDia += valorContabil;

                Update(saldoHistorico);
            }
        }
    }
}

