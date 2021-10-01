using AutoMapper;
using Evian.Entities.Entities.DTO;
using System;
using System.Collections.Generic;

namespace Evian.Entities.Entities.Adapters
{
    public class EstadoProfile : Profile
    {
        public EstadoProfile()
        {
            CreateMap<EstadoDTO, Estado>()
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
