using AutoMapper;
using Banco.WebApi.DTOs.Requests;
using Banco.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.WebApi.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<ClienteAddDTO, Cliente>();
            CreateMap<ClienteUpdateDTO, Cliente>();
        }
    }
}
