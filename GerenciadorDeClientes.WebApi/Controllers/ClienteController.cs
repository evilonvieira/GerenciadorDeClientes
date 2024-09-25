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
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IConfiguration _config;
        private readonly IClienteService _clienteService;

        public ClienteController(ILogger<ClienteController> logger, IConfiguration config, IClienteService clienteService)
        {
            _logger = logger;
            _config = config;
            _clienteService = clienteService;
        }

        [Authorize, HttpGet("listar/{pagina}/{registrosPorPagina}")]
        public async Task<ResultadoOperacao<PaginacaoLista<Cliente>>> ListarClientePaginado(int pagina, int registrosPorPagina)
        {
            try
            {
                var registros = await _clienteService.ListarPaginadoAsync(pagina, registrosPorPagina);
                return ResultadoOperacao<PaginacaoLista<Cliente>>.CriarResultadoComSucesso(registros);
            }
            catch (Exception error)
            {
                return ResultadoOperacao<PaginacaoLista<Cliente>>.CriarResultadoComFalha(error.Message);
            }
        }

        [Authorize, HttpGet("pesquisar/{id}")]
        public async Task<ResultadoOperacao<Cliente>> ListarCliente(long id) 
        {
            try
            {
                var registro = await _clienteService.ListarAsync(id);
                return ResultadoOperacao<Cliente>.CriarResultadoComSucesso(registro);
            }
            catch (Exception error)
            {
                return ResultadoOperacao<Cliente>.CriarResultadoComFalha(error.Message);
            }
        }

        [Authorize, HttpDelete("excluir/{id}")]
        public async Task<ResultadoOperacao> ExcluirCliente(long id)
        {
            try
            {
                await _clienteService.ExcluirAsync(id);
                return ResultadoOperacao.CriarResultadoComSucesso();
            }
            catch (Exception error)
            {
                return ResultadoOperacao.CriarResultadoComFalha(error.Message);
            }
        }

        [Authorize, HttpGet("validar/duplicidade/email/{id}/{email}")]
        public async Task<ResultadoOperacao<bool>> ValidarEmailJaCadastrado(long id, string email)
        {
            try
            {
                var resultado = await _clienteService.ValidarDuplicidadeDeEmailAsync(email, id);
                return ResultadoOperacao<bool>.CriarResultadoComSucesso(resultado);
            }
            catch (Exception error)
            {
                return ResultadoOperacao<bool>.CriarResultadoComFalha(error.Message);
            }
        }

        [Authorize, HttpPost("salvar")]
        public async Task<ResultadoOperacao<Cliente>> SalvarCliente([FromBody] ClienteDTO cliente)
        {
            try
            {
                var resultado = await _clienteService.ManterAsync(cliente);
                return ResultadoOperacao<Cliente>.CriarResultadoComSucesso(resultado);
            }
            catch (Exception error)
            {
                return ResultadoOperacao<Cliente>.CriarResultadoComFalha(error.Message);
            }
        }
    }

    public class ClienteModel {
        public required string Username { get; set; } 
        public required string Password { get; set; } 
    }
}
