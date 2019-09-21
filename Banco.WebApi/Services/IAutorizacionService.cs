using Banco.WebApi.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.WebApi.Services
{
    public interface IAutorizacionService
    {
        Task<string> Login(LoginDTO loginDTO);
    }
}
