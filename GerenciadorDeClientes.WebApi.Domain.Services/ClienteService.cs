using AutoMapper;
using GerenciadorDeClientes.Infra.Core.Util;
using GerenciadorDeClientes.WebApi.Application.DTOs;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Services;
using GerenciadorDeClientes.WebApi.Domain.Entities;
using System.Reflection;

namespace GerenciadorDeClientes.WebApi.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IUnitOfWork unitOfWork, IClienteRepository clienteRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
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
            var entity = await _clienteRepository.GetByIdAsync((int)id);
            return entity;
        }

        public async Task<Cliente> ListarComEnderecoAsync(long id)
        {
            var entity = await _clienteRepository.PerquisarComEnderecosAsync((int)id);
            return entity;
        }

        public async Task<PaginacaoLista<Cliente>> ListarPaginadoAsync(int pagina, int qtdRegistrosPorPagina)
        {
            return await _clienteRepository.ListaPaginadoAsync(pagina, qtdRegistrosPorPagina);
        }

        public async Task<Cliente?> ManterAsync(ClienteDTO clienteDTO)
        {
            ValidaCliente(clienteDTO);

            var emailInvalido = await ValidarDuplicidadeDeEmailAsync(clienteDTO.Email, clienteDTO.Id);
            if (emailInvalido)
                throw new Exception("E-mail ja cadastrado no sistema");

            var entity = _mapper.Map<Cliente>(clienteDTO);
            if (!string.IsNullOrEmpty(clienteDTO.LogotipoModificado))
            {
                var base64 = clienteDTO.LogotipoModificado?
                    .Replace("data:image/jpeg;base64,", "")
                    .Replace("data:image/png;base64,", "")
                    .Replace("data:image/jpg;base64,", "")
                    ;
                if (!string.IsNullOrWhiteSpace(base64))
                    entity.Logo = Convert.FromBase64String(base64);
            }

            if (clienteDTO.Id == 0)
                return await _clienteRepository.InserirAsync(entity);
            else
                return await _clienteRepository.AtualizarAsync(entity);
        }

        public async Task<bool> ValidarDuplicidadeDeEmailAsync(string email, long id)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("E-mail informado inválido");

            var resultado = await _clienteRepository.ValidarDuplicidadeDeEmailAsync(email, id);
            return resultado;
        }

        private void ValidaCliente(ClienteDTO cliente)
        {
            if (cliente == null)
                throw new Exception("Valores de entrada inválidos");

            if (string.IsNullOrWhiteSpace(cliente.Email))
                throw new Exception("E-mail não informado");

            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new Exception("Nome não informada");
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
