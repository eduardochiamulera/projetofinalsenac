using AutoMapper;
using Evian.Entities.Entities.DTO;
using System;
using System.Collections.Generic;

namespace Evian.Entities.Entities.Adapters
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<PessoaDTO, Pessoa>()
                .ForMember(p => p.DataInclusao, option => option.Ignore())
                .ForMember(p => p.DataAlteracao, option => option.Ignore())
                .ForMember(p => p.DataExclusao, option => option.Ignore())
                .ForMember(p => p.UsuarioInclusao, option => option.Ignore())
                .ForMember(p => p.UsuarioAlteracao, option => option.Ignore())
                .ForMember(p => p.UsuarioExclusao, option => option.Ignore())
                .ForMember(p => p.Ativo, option => option.Ignore())
                .ForMember(p => p.Notification, option => option.Ignore())
                .ReverseMap();

            CreateMap<Pessoa, PessoaDTO>()
                .ForMember(p => p.PaisNome, m => m.MapFrom(a => a.Pais.Nome))
                .ForMember(p => p.CidadeNome, m => m.MapFrom(a => a.Cidade.Nome))
                .ForMember(p => p.EstadoNome, m => m.MapFrom(a => a.Estado.Nome));
        }
    }
}
