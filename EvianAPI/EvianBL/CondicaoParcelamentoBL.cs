using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using Evian.Notifications;
using Evian.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvianBL
{
    public class CondicaoParcelamentoBL : EmpresaBL<CondicaoParcelamento>
    {
        public CondicaoParcelamentoBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void ValidaModel(CondicaoParcelamento entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.CondicoesParcelamento) && entity.CondicoesParcelamento.EndsWith(","))
                entity.CondicoesParcelamento = entity.CondicoesParcelamento.Substring(0, entity.CondicoesParcelamento.Length - 1);

            entity.Fail(!entity.QtdParcelas.HasValue && string.IsNullOrEmpty(entity.CondicoesParcelamento), CondicaoParcelamentoNaoInformada);
            entity.Fail(entity.QtdParcelas.HasValue && (entity.QtdParcelas < 1 || entity.QtdParcelas > 100), QtdParcelasInvalida);
            entity.Fail(!entity.QtdParcelas.HasValue && !ValidaFormatoCondicoesParcelamento(entity.CondicoesParcelamento), CondicaoParcelamentoInvalida);
            entity.Fail(All.Any(x => x.Id != entity.Id && x.Descricao.ToUpper() == entity.Descricao.ToUpper()), 
                new Error("Descrição da condição de parcelamento já utilizada anteriormente.", "descricao", All.FirstOrDefault(x => x.Id != entity.Id && x.Descricao.ToUpper() == entity.Descricao.ToUpper())?.Id.ToString()));


            base.ValidaModel(entity);
        }

        private bool ValidaFormatoCondicoesParcelamento(string condParcelamento)
        {
            if (!string.IsNullOrWhiteSpace(condParcelamento))
            {
                foreach (var itemCondicao in condParcelamento.Split(',').ToList())
                {
                    var valueOut = default(int);

                    if (!int.TryParse(itemCondicao, out valueOut))
                        return false;
                }
            }

            return true;
        }

        private List<CondicaoParcelamentoParcelaDTO> GetPrestacaoUnica(DateTime dataBase, decimal valor, int daysAdd)
        {
            return new List<CondicaoParcelamentoParcelaDTO>()
            {
                new CondicaoParcelamentoParcelaDTO()
                {
                    DescricaoParcela = "01/01",
                    Valor = valor,
                    DataVencimento = dataBase.AddDays(daysAdd)
                }
            };
        }

        private List<CondicaoParcelamentoParcelaDTO> GetPrestacoesMultiplasPorMes(DateTime dataBase, int qtdParcelas, decimal valor)
        {
            var result = new List<CondicaoParcelamentoParcelaDTO>();

            var basePow = (decimal)Math.Pow(10, 2);
            var valorParcela = Math.Truncate(valor / qtdParcelas * basePow) / basePow;
            var valorUltimaParcela = Math.Round(valor - (valorParcela * (qtdParcelas - 1)), 2);

            for (int iParcela = 1; iParcela <= qtdParcelas; iParcela++)
            {
                result.Add(new CondicaoParcelamentoParcelaDTO()
                {
                    DescricaoParcela = string.Format("{0}/{1}", iParcela.ToString("D2"), qtdParcelas.ToString("D2")),
                    DataVencimento = iParcela > 1
                        ? dataBase.AddMonths(iParcela - 1)
                        : dataBase,
                    Valor = iParcela == qtdParcelas
                        ? valorUltimaParcela
                        : valorParcela
                });
            }

            return result;
        }

        private List<CondicaoParcelamentoParcelaDTO> GetPrestacoesMultiplasPorDia(DateTime dataBase, List<int> daysToAdd, decimal valor)
        {
            var result = new List<CondicaoParcelamentoParcelaDTO>();

            var qtdParcelas = daysToAdd.Count();
            var basePow = (decimal)Math.Pow(10, 2);
            var valorParcela = (decimal)Math.Truncate(valor / qtdParcelas * basePow) / basePow;
            var valorUltimaParcela = (decimal)Math.Round(valor - (valorParcela * (qtdParcelas - 1)), 2);

            for (int iParcela = 1; iParcela <= qtdParcelas; iParcela++)
            {
                result.Add(new CondicaoParcelamentoParcelaDTO()
                {
                    DescricaoParcela = string.Format("{0}/{1}", iParcela.ToString("D2"), qtdParcelas.ToString("D2")),
                    DataVencimento = dataBase.AddDays(daysToAdd[iParcela - 1]),
                    Valor = iParcela == qtdParcelas
                        ? valorUltimaParcela
                        : valorParcela
                });
            }

            return result;
        }

        public List<CondicaoParcelamentoParcelaDTO> GetPrestacoes(CondicaoParcelamento condicaoParcelamento, DateTime dataVencimentoBase, decimal valorTotal)
        {
            if (condicaoParcelamento == null)
                throw new Exception("Condição de Parcelamento inválida.");

            var qtdParcelasFixas = condicaoParcelamento.QtdParcelas.HasValue
                ? ((int)condicaoParcelamento.QtdParcelas)
                : default(int);

            var condicoesParcelamento = string.IsNullOrEmpty(condicaoParcelamento.CondicoesParcelamento)
                ? new List<int>()
                : condicaoParcelamento.CondicoesParcelamento.Split(',').Select(x => Convert.ToInt32(x)).ToList();

            if (qtdParcelasFixas == 1 || condicoesParcelamento.Count() == 1)
                return GetPrestacaoUnica(dataVencimentoBase, valorTotal, condicoesParcelamento.Count() == 1 ? condicoesParcelamento.FirstOrDefault() : 0);

            if (qtdParcelasFixas > 1)
                return GetPrestacoesMultiplasPorMes(dataVencimentoBase, qtdParcelasFixas, valorTotal);

            return GetPrestacoesMultiplasPorDia(dataVencimentoBase, condicoesParcelamento, valorTotal);
        }

        public List<CondicaoParcelamentoParcelaDTO> GetPrestacoes(Guid condicaoPagamentoId, DateTime dataVencimentoBase, decimal valorTotal)
        {
            var condicaoParcelamento = Find(condicaoPagamentoId);
            return GetPrestacoes(condicaoParcelamento, dataVencimentoBase, valorTotal);
        }

        public static Error CondicaoParcelamentoNaoInformada = new Error("Informe a quantidade de parcelas ou o intervalo de dias.");
        public static Error QtdParcelasInvalida = new Error("Quantidade de parcelas inválida.", "qtdParcelas");
        public static Error CondicaoParcelamentoInvalida = new Error("Condição de parcelamento inválida.", "condicoesParcelamento");
    }
}
