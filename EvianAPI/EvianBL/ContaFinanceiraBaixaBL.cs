using Evian.Entities.Entities;
using Evian.Entities.Enums;
using Evian.Notifications;
using Evian.Repository.Core;
using System;
using System.Collections.Generic;
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

            contaFinanceira.ValorPago = contaFinanceira.ValorPago.HasValue ? contaFinanceira.ValorPago + entity.Valor : entity.Valor;

            contaFinanceira.Saldo -= entity.Valor;
            entity.Data = DateTime.Now;

            if (contaFinanceira == null) throw new Exception("Conta inválida.");

            entity.Fail(!_unitOfWork.ContaBancariaBL.All.Any(x => x.Id == entity.ContaBancariaId), ContaInvalida);
            entity.Fail((Math.Round(entity.Valor, 2) > Math.Round(contaFinanceira.ValorPrevisto, 2)) || entity.Valor <= 0, ValorPagoInvalido);

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

        public override void Delete(ContaFinanceiraBaixa entity)
        {
            var contaFinanceira = _unitOfWork.ContaFinanceiraBL.All.FirstOrDefault(x => x.Id == entity.ContaFinanceiraId);
            var valorPagoConta = contaFinanceira.ValorPago.HasValue ? (decimal)contaFinanceira.ValorPago : default(decimal);

            var valorBaixa = (entity.Valor * -1);
            valorPagoConta += valorBaixa;

            //Atualiza Conta Financeira
            contaFinanceira.ValorPago = valorPagoConta;
            contaFinanceira.Saldo = contaFinanceira.ValorPrevisto;

            if (contaFinanceira.ValorPago > default(decimal))
                contaFinanceira.StatusContaBancaria = StatusContaBancaria.BaixadoParcialmente;
            else
                contaFinanceira.StatusContaBancaria = StatusContaBancaria.EmAberto;

            //Atualiza Saldo Histórico
            _unitOfWork.SaldoHistoricoBL.AtualizaSaldoHistorico(entity.Data, valorBaixa, entity.ContaBancariaId, contaFinanceira.TipoContaFinanceira);

            string descricao = "Estorno* " + contaFinanceira.Descricao;
            //Atualiza movimentações
            _unitOfWork.MovimentacaoBL.CriaMovimentacao(DateTime.Now, valorBaixa, entity.ContaBancariaId, contaFinanceira.TipoContaFinanceira, entity.ContaFinanceiraId.Value, descricao);

            base.Delete(entity);
        }

        public void DeletarTodasBaixas(List<ContaFinanceiraBaixa> entitiesToDelete)
        {
            foreach (var item in entitiesToDelete)
            {
                Delete(item);
            }
        }

        public static Error ContaInvalida = new Error("Conta Bancária inválida.");
        public static Error ValorPagoInvalido = new Error("Valor pago não pode ser superior ao valor da conta.");
        public static Error SomaValoresInvalida = new Error("Somatório dos valores não pode ser superior ao valor da conta.");
    }
}