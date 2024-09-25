using GerenciadorDeClientes.Infra.Data.Context;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories;
using GerenciadorDeClientes.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Infra.Data.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> ObterPorEmailSenhaAsync(string? email, string? senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Senha == senha);
        }

        public async Task<bool> ValidarDuplicidadeDeEmailAsync(string email, long id)
        {
            if (id == 0)
                return await _context.Clientes.AnyAsync(c => c.Email == email);
            else
                return await _context.Clientes.AnyAsync(c => c.Email == email && c.Id != id);
        }
    }
}
