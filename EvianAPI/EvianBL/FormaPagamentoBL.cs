using Evian.Entities;
using Evian.Notifications;
using Evian.Repository.Core;
using System.Linq;

namespace EvianBL
{
    public class FormaPagamentoBL : EmpresaBL<FormaPagamento>
    {
        public FormaPagamentoBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void ValidaModel(FormaPagamento entity)
        {
            entity.Fail(All.Any(x => x.Ativo && x.Id != entity.Id && x.Descricao.ToUpper() == entity.Descricao.ToUpper() && x.TipoFormaPagamento == entity.TipoFormaPagamento),
                new Error("Descrição da forma de pagamento já utilizada anteriormente.", "descricao", All.FirstOrDefault(x => x.Id != entity.Id && x.Descricao.ToUpper() == entity.Descricao.ToUpper() && x.TipoFormaPagamento == entity.TipoFormaPagamento)?.Id.ToString()));

            base.ValidaModel(entity);
        }
    }
}