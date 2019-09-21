using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco.WebApi.Data;
using Banco.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Banco.WebApi.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Cliente cliente)
        {
            cliente.FechaAlta = DateTime.Now;
            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cliente>> GetAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cliente> GetAsync(int Id)
        {
            return await _context.Clientes
                .FindAsync(Id);
        }

        public async Task<Cliente> GetByCuentatAsync(int Id, int IdCuenta)
        {
            return await _context.Clientes
                .Include(x => x.Cuentas)
                .Where(x => x.Cuentas.Any(y => y.Id == IdCuenta))
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Cliente>> SearchAsync(string apellido, string nombre, int pageSize, int pageNumber)
        {
            return await _context.Clientes
                .Where(c => 
                    c.Apellido.Contains(apellido, StringComparison.InvariantCultureIgnoreCase)
                    && c.Nombre.Contains(nombre, StringComparison.InvariantCultureIgnoreCase))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
