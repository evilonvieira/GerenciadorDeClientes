using GerenciadorDeClientes.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Usuario?> ObterPorEmailSenhaAsync(string? email, string? senha);
        Task<bool> ValidarDuplicidadeDeEmailAsync(string email, long id);
        Task<Cliente?> InserirAsync(Cliente novoCliente);
        Task<Cliente?> AtualizarAsync(Cliente cliente);
    }
}
