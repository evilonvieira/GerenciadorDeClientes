using GerenciadorDeClientes.Infra.CrossCutting;
using GerenciadorDeClientes.WebApi.Application.DTOs;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GerenciadorDeClientes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;
        private readonly IUsuarioService _usuarioService;

        public AuthController(ILogger<AuthController> logger, IConfiguration config, IUsuarioService usuarioService)
        {
            _logger = logger;
            _config = config;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<ResultadoOperacao<TokenResponse>> Login([FromBody] LoginDTO login)
        {
            try
            {
                var bearerToken = await _usuarioService.EfetuarLoginAsync(login);
                if (string.IsNullOrWhiteSpace(bearerToken))
                    throw new Exception("E-mail e/ou Senha inválidos");

                return ResultadoOperacao<TokenResponse>.CriarResultadoComSucesso(
                    new TokenResponse { Token = bearerToken }
                );
            }
            catch (Exception error)
            {
                return ResultadoOperacao<TokenResponse>.CriarResultadoComFalha(error.Message);
            }
        }

    }


}
