using AutoMapper;
using Evian.Entities.Entities.DTO;

namespace Evian.Entities.Entities.Adapters
{
    public class ContaFinanceiraProfile : Profile
    {
        public ContaFinanceiraProfile()
        {
            CreateMap<ContaFinanceiraDTO, ContaFinanceira>()
                .ForMember(p => p.DataInclusao, option => option.Ignore())
                .ForMember(p => p.DataAlteracao, option => option.Ignore())
                .ForMember(p => p.DataExclusao, option => option.Ignore())
                .ForMember(p => p.UsuarioInclusao, option => option.Ignore())
                .ForMember(p => p.UsuarioAlteracao, option => option.Ignore())
                .ForMember(p => p.UsuarioExclusao, option => option.Ignore())
                .ForMember(p => p.Ativo, option => option.Ignore())
                .ForMember(p => p.Notification, option => option.Ignore())
                .ReverseMap();

            CreateMap<ContaFinanceira, ContaFinanceiraDTO>()
               .ForMember(p => p.CategoriaNome, m => m.MapFrom(a => a.Categoria.Descricao))
               .ForMember(p => p.CondicaoParcelamentoNome, m => m.MapFrom(a => a.CondicaoParcelamento.Descricao))
               .ForMember(p => p.FormaPagamentoNome, m => m.MapFrom(a => a.FormaPagamento.Descricao))
               .ForMember(p => p.PessoaNome, m => m.MapFrom(a => a.Pessoa.Nome));

        }
    }
}
