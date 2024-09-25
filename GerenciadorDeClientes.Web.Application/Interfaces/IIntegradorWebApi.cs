using GerenciadorDeClientes.Infra.CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Web.Application.Interfaces
{
    public interface IIntegradorWebApi
    {
        Task<ResultadoOperacao<T>> PostAsync<T>(string url, string method, string? bearerToken = null, object? body = null);
        Task<ResultadoOperacao<T>> GetAsync<T>(string url, string method, string? bearerToken = null);
        Task<ResultadoOperacao> DeleteAsync(string url, string method, string bearerToken, long id);
    }
}
