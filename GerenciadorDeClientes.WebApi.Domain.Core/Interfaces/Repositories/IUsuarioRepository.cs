using GerenciadorDeClientes.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {
        Task<Usuario?> ObterPorEmailSenhaAsync(string? email, string? senha);
    }
}
