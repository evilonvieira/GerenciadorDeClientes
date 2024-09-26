using GerenciadorDeClientes.Infra.CrossCutting.Security;
using GerenciadorDeClientes.WebApi.Application.DTOs;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Services;
using GerenciadorDeClientes.WebApi.Domain.Entities;

namespace GerenciadorDeClientes.WebApi.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IJwtTokenService jwtTokenService)
        {
            _usuarioRepository = usuarioRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> EfetuarLoginAsync(LoginDTO loginDTO)
        {
            ValidaLogin(loginDTO);


            var senhaCriptografada = PasswordCriptor.Encrypt(loginDTO?.Senha);


            //busca o usuario na base de dados
            var usuario = await _usuarioRepository.ObterPorEmailSenhaAsync(loginDTO?.Email, senhaCriptografada);
            if (usuario == null)
                return string.Empty;


            //gera o token com os dados do usuário
            var token = _jwtTokenService.GerarBearerToken(usuario);


            return token;
        }

        private void ValidaLogin(LoginDTO login)
        {
            if (login == null)
                throw new Exception("Valores de entrada inválidos");

            if (string.IsNullOrWhiteSpace(login.Email))
                throw new Exception("E-mail não informado");

            if (string.IsNullOrWhiteSpace(login.Senha))
                throw new Exception("Senha não informada");
        }
    }
}
