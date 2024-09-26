using AutoMapper;
using GerenciadorDeClientes.WebApi.Application.DTOs;
using GerenciadorDeClientes.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.WebApi.Domain.AutoMapper
{
    public class ClienteDTO_to_ClienteEntity : Profile
    {
        public ClienteDTO_to_ClienteEntity()
        {
            CreateMap<ClienteDTO, Cliente>()
                .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.LogotipoModificado) ? src.Logotipo : src.LogotipoModificado))
                
                ;
        }
    }
}
