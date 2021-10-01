using AutoMapper;
using Evian.Entities.Entities.DTO;

namespace Evian.Entities.Entities.Adapters
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CategoriaDTO, Categoria>()
                .ForMember(p => p.DataInclusao, option => option.Ignore())
                .ForMember(p => p.DataAlteracao, option => option.Ignore())
                .ForMember(p => p.DataExclusao, option => option.Ignore())
                .ForMember(p => p.UsuarioInclusao, option => option.Ignore())
                .ForMember(p => p.UsuarioAlteracao, option => option.Ignore())
                .ForMember(p => p.UsuarioExclusao, option => option.Ignore())
                .ForMember(p => p.Ativo, option => option.Ignore())
                .ForMember(p => p.Notification, option => option.Ignore())
                .ForMember(p => p.Id, option => option.Ignore())
                .ReverseMap();
        }
    }
}
