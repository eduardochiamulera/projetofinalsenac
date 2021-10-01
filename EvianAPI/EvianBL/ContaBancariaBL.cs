using Evian.Entities.Entities;
using Evian.Notifications;
using Evian.Repository.Core;
using System.Linq;

namespace EvianBL
{
    public class ContaBancariaBL : EmpresaBL<ContaBancaria>
    {
        public ContaBancariaBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }   

        public override void ValidaModel(ContaBancaria entity)
        {
            entity.Fail(All.Any(x => x.Id != entity.Id && x.NomeConta.ToUpper() == entity.NomeConta.ToUpper()), 
                new Error("Descrição da conta bancária já utilizada anteriormente.", "nomeConta", All.FirstOrDefault(x => x.Id != entity.Id && x.NomeConta.ToUpper() == entity.NomeConta.ToUpper())?.Id.ToString()));

            var banco = _unitOfWork.BancoBL.All.FirstOrDefault(x => x.Id == entity.BancoId);
            if(banco != null && banco.Codigo != "999")
            {
                var dadosAgenciaContaInvalid = string.IsNullOrWhiteSpace(entity.Agencia) ||
                    string.IsNullOrWhiteSpace(entity.Conta) ||
                    string.IsNullOrWhiteSpace(entity.DigitoConta);

                entity.Fail(dadosAgenciaContaInvalid, AgenciaContaObrigatoria);
            }

            base.ValidaModel(entity);
        }

        public override void Insert(ContaBancaria entity)
        {            
            base.Insert(entity);

            _unitOfWork.SaldoHistoricoBL.InsereSaldoInicial(entity.Id, entity.ValorInicial == null ? 0 : entity.ValorInicial);
        }

        public static Error DescricaoDuplicada = new Error("Descrição da conta bancária já utilizada anteriormente.", "nomeConta");
        public static Error AgenciaContaObrigatoria = new Error("Dados de agência e conta são obrigatórios.", "agencia");
        public static Error BancoInvalido = new Error("Código do Banco inválido.", "bancoId");
    }
}