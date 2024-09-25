using GerenciadorDeClientes.Infra.Core.Util;
using GerenciadorDeClientes.WebApi.Application.DTOs;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Services;
using GerenciadorDeClientes.WebApi.Domain.Entities;

namespace GerenciadorDeClientes.WebApi.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IUnitOfWork unitOfWork, IClienteRepository clienteRepository)
        {
            _unitOfWork = unitOfWork;
            _clienteRepository = clienteRepository;
        }


        public async Task<string> EfetuarLoginAsync(LoginDTO loginDTO)
        {

            //_clienteRepository.ListaPaginadoAsync()
            //_unitOfWork.

            /*
            ValidaLogin(loginDTO);

            //busca o usuario na base de dados
            var usuario = await _usuarioRepository.ObterPorEmailSenhaAsync(loginDTO?.Email, loginDTO?.Senha);
            if (usuario == null)
                return string.Empty;


            //gera o token com os dados do usuário
            var token = _jwtTokenService.GerarBearerToken(usuario);
            */

            return string.Empty;
        }

        public async Task ExcluirAsync(long id)
        {
            var entity = await ListarAsync(id);
            if (entity != null)
                _clienteRepository.Delete(entity);
            await _clienteRepository.SaveChangesAsync();
        }

        public async Task<Cliente> ListarAsync(long id)
        {
            return await _clienteRepository.GetByIdAsync((int)id);
        }

        public async Task<PaginacaoLista<Cliente>> ListarPaginadoAsync(int pagina, int qtdRegistrosPorPagina)
        {
            return await _clienteRepository.ListaPaginadoAsync(pagina, qtdRegistrosPorPagina);
        }

        public async Task<bool> ValidarDuplicidadeDeEmailAsync(string email, long id)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("E-mail informado inválido");

            return await _clienteRepository.ValidarDuplicidadeDeEmailAsync(email, id);
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
