using GerenciadorDeClientes.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories
{
    public interface IEnderecoClienteRepository : IRepository<Endereco>
    {
        Task<bool> ValidarDuplicidadeDeEmailAsync(string email, long id);
        Task<Endereco?> InserirAsync(Endereco novoCliente);
        Task<Endereco?> AtualizarAsync(Endereco cliente);
        Task ExcluirAsync(long id);
    }
}
