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
                .ForMember(p => p.Id, option => option.Ignore())
                .ReverseMap();

            //CreateMap<Pessoa, PessoaDTO>();
        }
    }
}
