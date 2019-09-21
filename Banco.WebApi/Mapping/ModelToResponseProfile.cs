using AutoMapper;
using Banco.WebApi.DTOs.Responses;
using Banco.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.WebApi.Mapping
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<Cliente, ClienteDTO>();
        }
    }
}
