using Banco.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.WebApi.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAsync();
        Task<Cliente> GetAsync(int Id);
        Task<Cliente> GetByCuentatAsync(int Id, int IdCuenta); 
        Task AddAsync(Cliente cliente);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Cliente cliente);

        Task<IEnumerable<Cliente>> SearchAsync(string apellido, string nombre, int pageSize, int pageNumber);


    }
}
