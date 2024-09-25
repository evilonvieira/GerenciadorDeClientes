using GerenciadorDeClientes.Infra.Core.Util;
using GerenciadorDeClientes.WebApi.Application.DTOs;
using GerenciadorDeClientes.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Services
{
    public interface IEnderecoClienteService
    {
        
        //Task<PaginacaoLista<Cliente>> ListarPaginadoAsync(int pagina, int qtdRegistrosPorPagina);
        Task<Endereco> ListarAsync(long id);
        Task<bool> ValidarDuplicidadeDeEmailAsync(string email, long id);
        Task ExcluirAsync(long id);
        Task<Endereco> ManterAsync(EnderecoDTO enderecoDTO);
    }
}
