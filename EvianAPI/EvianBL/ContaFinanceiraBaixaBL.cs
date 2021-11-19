using Evian.Entities.Entities;
using Evian.Entities.Enums;
using Evian.Notifications;
using Evian.Repository.Core;
using System;
using System.Linq;

namespace EvianBL
{
    public class ContaFinanceiraBaixaBL : EmpresaBL<ContaFinanceiraBaixa>
    {

        public ContaFinanceiraBaixaBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void Insert(ContaFinanceiraBaixa entity)
        {
            var contaFinanceira = entity.ContaFinanceiraId != default(Guid) ? 
                _unitOfWork.ContaFinanceiraBL.All.FirstOrDefault(x => x.Id == entity.ContaFinanceiraId) : throw new Exception("Conta inválida.");

            contaFinanceira.ValorPago += entity.Valor;

            if (contaFinanceira.ValorPago > contaFinanceira.ValorPrevisto) throw new Exception("Valor pago deve ser menor ou igual ao saldo");

            contaFinanceira.Saldo -= entity.Valor;

            entity.ContaFinanceira = null;
            contaFinanceira.ValorPrevisto = Math.Round(contaFinanceira.ValorPrevisto, 2);

            if (contaFinanceira == null) throw new Exception("Conta inválida.");

            entity.ContaFinanceiraId = contaFinanceira.Id;

            entity.Fail(!_unitOfWork.ContaBancariaBL.All.Any(x => x.Id == entity.ContaBancariaId), ContaInvalida);
            entity.Fail(Math.Round(entity.Valor, 2) > Math.Round(contaFinanceira.ValorPrevisto, 2), ValorPagoInvalido);

            base.Insert(entity);

            if (Math.Round(contaFinanceira.ValorPago.GetValueOrDefault(0), 2) < Math.Round(contaFinanceira.ValorPrevisto, 2))
                contaFinanceira.StatusContaBancaria = StatusContaBancaria.BaixadoParcialmente;
            else
                contaFinanceira.StatusContaBancaria = StatusContaBancaria.Pago;

            _unitOfWork.ContaFinanceiraBL.Update(contaFinanceira);

            _unitOfWork.SaldoHistoricoBL.AtualizaSaldoHistorico(entity.Data, entity.Valor, entity.ContaBancariaId, contaFinanceira.TipoContaFinanceira);

            _unitOfWork.MovimentacaoBL.CriaMovimentacao(entity.Data, entity.Valor, entity.ContaBancariaId, contaFinanceira.TipoContaFinanceira, entity.ContaFinanceiraId.Value);
        }

        public override void Update(ContaFinanceiraBaixa entity)
        {
            throw new Exception("Não é possível alterar uma baixa.");
        }

        public void DeleteWithoutRecalc(ContaFinanceiraBaixa entity)
        {
            base.Delete(entity);
        }

        //public override void Delete(ContaFinanceiraBaixa entity)
        //{
        //    //var contaFinanceira = contaFinanceiraBL.All.FirstOrDefault(x => x.Id == entity.ContaFinanceiraId);
        //    var valorPagoConta = contaFinanceira.ValorPago.HasValue ? (double)contaFinanceira.ValorPago : default(double);

        //    var valorBaixa = (entity.Valor * -1);
        //    valorPagoConta += valorBaixa;

        //    //Atualiza Conta Financeira
        //    contaFinanceira.ValorPago = valorPagoConta;
        //    if (contaFinanceira.ValorPago > default(double))
        //        contaFinanceira.StatusContaBancaria = StatusContaBancaria.BaixadoParcialmente;
        //    else
        //        contaFinanceira.StatusContaBancaria = StatusContaBancaria.EmAberto;

        //    //Atualiza Saldo Histórico
        //    saldoHistoricoBL.AtualizaSaldoHistorico(entity.Data, valorBaixa, entity.ContaBancariaId, contaFinanceira.TipoContaFinanceira);

        //    string descricao = "Estorno* " + contaFinanceira.Descricao;
        //    //Atualiza movimentações
        //    movimentacaoBL.CriaMovimentacao(DateTime.Now, valorBaixa, entity.ContaBancariaId, contaFinanceira.TipoContaFinanceira, entity.ContaFinanceiraId, descricao);

        //    base.Delete(entity);
        //}

        //public void GeraContaFinanceiraBaixa(ContaReceber contaFinanceira)
        //{
        //    if (contaFinanceira.Id == default(Guid)) throw new Exception("Conta Financeira inválida.");

        //    if (contaFinanceira.ContaBancariaId != default(Guid))
        //        contaFinanceira.ContaBancaria = contaBancariaBL.Find(contaFinanceira.ContaBancariaId);
        //    else
        //    {
        //        var bancoOutros = bancoBL.All.FirstOrDefault(x => x.Codigo == "999");

        //        contaFinanceira.ContaBancariaId = contaBancariaBL.All.FirstOrDefault(x => x.BancoId == bancoOutros.Id).Id;
        //    }

        //    if (contaFinanceira.ContaBancaria == null && contaFinanceira.ContaBancariaId == null) throw new Exception("Conta bancária inválida.");

        //    saldoHistoricoBL.AtualizaSaldoHistorico(contaFinanceira.DataVencimento, contaFinanceira.ValorPrevisto, contaFinanceira.ContaBancariaId, contaFinanceira.TipoContaFinanceira);

        //    movimentacaoBL.CriaMovimentacao(contaFinanceira.DataVencimento, contaFinanceira.ValorPrevisto, contaFinanceira.ContaBancariaId, contaFinanceira.TipoContaFinanceira, contaFinanceira.Id);

        //    base.Insert(new ContaFinanceiraBaixa()
        //    {
        //        Data = contaFinanceira.DataVencimento,
        //        ContaFinanceiraId = contaFinanceira.Id,
        //        ContaBancariaId = contaFinanceira.ContaBancariaId,
        //        Valor = contaFinanceira.ValorPago.HasValue ? contaFinanceira.ValorPago.Value : contaFinanceira.ValorPrevisto,
        //        Observacao = contaFinanceira.Descricao
        //    });
        //}

        public static Error ContaInvalida = new Error("Conta Bancária inválida.");
        public static Error ValorPagoInvalido = new Error("Valor pago não pode ser superior ao valor da conta.");
        public static Error SomaValoresInvalida = new Error("Somatório dos valores não pode ser superior ao valor da conta.");
    }
}