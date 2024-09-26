using GerenciadorDeClientes.Infra.Core.Util;
using GerenciadorDeClientes.Infra.CrossCutting;
using GerenciadorDeClientes.WebApi.Application.DTOs;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Services;
using GerenciadorDeClientes.WebApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GerenciadorDeClientes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly ILogger<EnderecoController> _logger;
        private readonly IConfiguration _config;
        private readonly IEnderecoClienteService _enderecoClienteService;

        public EnderecoController(ILogger<EnderecoController> logger, IConfiguration config, IEnderecoClienteService enderecoClienteService)
        {
            _logger = logger;
            _config = config;
            _enderecoClienteService = enderecoClienteService;
        }


        [Authorize, HttpDelete("excluir/{id}")]
        public async Task<ResultadoOperacao> ExcluirCliente(long id)
        {
            try
            {
                await _enderecoClienteService.ExcluirAsync(id);
                return ResultadoOperacao.CriarResultadoComSucesso();
            }
            catch (Exception error)
            {
                return ResultadoOperacao.CriarResultadoComFalha(error.Message);
            }
        }

        [Authorize, HttpPost("salvar")]
        public async Task<ResultadoOperacao<Endereco>> SalvarEnderecoCliente([FromBody] EnderecoDTO endereco)
        {
            try
            {
                var resultado = await _enderecoClienteService.ManterAsync(endereco);
                return ResultadoOperacao<Endereco>.CriarResultadoComSucesso(resultado);
            }
            catch (Exception error)
            {
                return ResultadoOperacao<Endereco>.CriarResultadoComFalha(error.Message);
            }
        }

        [Authorize, HttpGet("pesquisar/{id}")]
        public async Task<ResultadoOperacao<Endereco>> ListarCliente(long id)
        {
            try
            {
                var registro = await _enderecoClienteService.ListarAsync(id);
                return ResultadoOperacao<Endereco>.CriarResultadoComSucesso(registro);
            }
            catch (Exception error)
            {
                return ResultadoOperacao<Endereco>.CriarResultadoComFalha(error.Message);
            }
        }












        /*
        [Authorize, HttpGet("listar/{pagina}/{registrosPorPagina}")]
        public async Task<ResultadoOperacao<PaginacaoLista<Cliente>>> ListarClientePaginado(int pagina, int registrosPorPagina)
        {
            try
            {
                var registros = await _enderecoClienteService.ListarPaginadoAsync(pagina, registrosPorPagina);
                return ResultadoOperacao<PaginacaoLista<Cliente>>.CriarResultadoComSucesso(registros);
            }
            catch (Exception error)
            {
                return ResultadoOperacao<PaginacaoLista<Cliente>>.CriarResultadoComFalha(error.Message);
            }
        }


        [Authorize, HttpGet("pesquisar/enderecos/{id}")]
        public async Task<ResultadoOperacao<Cliente>> ListarClienteComEndereco(long id)
        {
            try
            {
                var registro = await _enderecoClienteService.ListarComEnderecoAsync(id);
                return ResultadoOperacao<Cliente>.CriarResultadoComSucesso(registro);
            }
            catch (Exception error)
            {
                return ResultadoOperacao<Cliente>.CriarResultadoComFalha(error.Message);
            }
        }


        [Authorize, HttpGet("validar/duplicidade/email/{id}/{email}")]
        public async Task<ResultadoOperacao<bool>> ValidarEmailJaCadastrado(long id, string email)
        {
            try
            {
                var resultado = await _enderecoClienteService.ValidarDuplicidadeDeEmailAsync(email, id);
                return ResultadoOperacao<bool>.CriarResultadoComSucesso(resultado);
            }
            catch (Exception error)
            {
                return ResultadoOperacao<bool>.CriarResultadoComFalha(error.Message);
            }
        }
        */

    }

}
