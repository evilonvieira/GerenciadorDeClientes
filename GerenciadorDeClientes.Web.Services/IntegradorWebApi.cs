using GerenciadorDeClientes.Infra.CrossCutting;
using GerenciadorDeClientes.Web.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;

namespace GerenciadorDeClientes.Web.Services
{
    public class IntegradorWebApi : IIntegradorWebApi
    {
        private readonly ILogger<IntegradorWebApi> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public IntegradorWebApi(ILogger<IntegradorWebApi> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<ResultadoOperacao<T>> GetAsync<T>(string url, string method, string? bearerToken = null)
        {
            try
            {
                using var client = _clientFactory.CreateClient();
                if (!string.IsNullOrWhiteSpace(bearerToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);


                using var response = await client.GetAsync($"{url}{method}");
                var contentResponse = await response.Content.ReadAsStringAsync();

                //Tratando cenário de erros
                if (!response.IsSuccessStatusCode)
                    return ResultadoOperacao<T>.CriarResultadoComFalha(contentResponse);


                //sucesso na comunicação e retorno
                var objetoRetorno = JsonConvert.DeserializeObject<T>(contentResponse);
                if (objetoRetorno == null)
                    return ResultadoOperacao<T>.CriarResultadoComFalha(contentResponse);

                return ResultadoOperacao<T>.CriarResultadoComSucesso(objetoRetorno);

            }
            catch (Exception error)
            {
                return ResultadoOperacao<T>.CriarResultadoComFalha(error.Message);
            }




            /*
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token não encontrado no cookie.");
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("https://localhost:7229/Cliente/list");
            IList<ClienteModel> Clientes = new List<ClienteModel>();
            if (response.IsSuccessStatusCode)
            {
                Clientes = await response.Content.ReadFromJsonAsync<List<ClienteModel>>();
            }
            */
        }

        public async Task<ResultadoOperacao<T>> PostAsync<T>(string url, string method, string? bearerToken = null, object? body = null)
        {
            try
            {
                //https://localhost:7229/auth/login
                using var client = _clientFactory.CreateClient();
                if (!string.IsNullOrWhiteSpace(bearerToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                
                
                using var response = await client.PostAsJsonAsync($"{url}{method}", body);
                var contentResponse = await response.Content.ReadAsStringAsync();

                //Tratando cenário de erros
                if (!response.IsSuccessStatusCode)
                    return ResultadoOperacao<T>.CriarResultadoComFalha(contentResponse);


                //sucesso na comunicação e retorno
                var objetoRetorno = JsonConvert.DeserializeObject<T>(contentResponse);
                if(objetoRetorno == null)
                    return ResultadoOperacao<T>.CriarResultadoComFalha(contentResponse);

                return ResultadoOperacao<T>.CriarResultadoComSucesso(objetoRetorno);

            }
            catch (Exception error)
            {
                return ResultadoOperacao<T>.CriarResultadoComFalha(error.Message);
            }
        }
    }
}
