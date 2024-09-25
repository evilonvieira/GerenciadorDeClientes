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
    public class EnderecoClienteRepository : Repository<Endereco>, IEnderecoClienteRepository
    {
        public EnderecoClienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Endereco?> AtualizarAsync(Endereco clienteModificado)
        {
            Update(clienteModificado);
            await SaveChangesAsync();
            return clienteModificado;
        }

        public async Task<Endereco?> InserirAsync(Endereco novoCliente)
        {
            novoCliente.Id = 0;
            await AddAsync(novoCliente);
            await SaveChangesAsync();
            return novoCliente;
        }


        public async Task<bool> ValidarDuplicidadeDeEmailAsync(string numero, long id)
        {
            if (id == 0)
                return await _context.Enderecos.AnyAsync(c => c.Numero == numero);
            else
                return await _context.Enderecos.AnyAsync(c => c.Numero == numero && c.Id != id);
        }
    }
}
