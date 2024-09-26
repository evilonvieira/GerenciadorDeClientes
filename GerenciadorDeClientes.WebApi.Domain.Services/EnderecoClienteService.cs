using AutoMapper;
using GerenciadorDeClientes.Infra.Core.Util;
using GerenciadorDeClientes.WebApi.Application.DTOs;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Services;
using GerenciadorDeClientes.WebApi.Domain.Entities;
using System.Reflection;

namespace GerenciadorDeClientes.WebApi.Domain.Services
{
    public class EnderecoClienteService : IEnderecoClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnderecoClienteRepository _enderecoClienteRepository;
        private readonly IMapper _mapper;

        public EnderecoClienteService(IUnitOfWork unitOfWork, IEnderecoClienteRepository enderecoClienteRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _enderecoClienteRepository = enderecoClienteRepository;
            _mapper = mapper;
        }


        public async Task ExcluirAsync(long id)
        {
            /*
            var entity = await ListarAsync(id);
            if (entity != null)
                _enderecoClienteRepository.Delete(entity);
            await _enderecoClienteRepository.SaveChangesAsync();
            */
            await _enderecoClienteRepository.ExcluirAsync(id);
        }

        public async Task<Endereco> ListarAsync(long id)
        {
            var entity = await _enderecoClienteRepository.GetByIdAsync((int)id);
            return entity;
        }


        public async Task<Endereco?> ManterAsync(EnderecoDTO clienteDTO)
        {
            ValidaEndereco(clienteDTO);

            /*
            var emailInvalido = await ValidarDuplicidadeDeEmailAsync(clienteDTO.Email, clienteDTO.Id);
            if (emailInvalido)
                throw new Exception("E-mail ja cadastrado no sistema");
            */

            if (clienteDTO.Id == 0)
                return await _enderecoClienteRepository.InserirAsync(_mapper.Map<Endereco>(clienteDTO));
            else
                return await _enderecoClienteRepository.AtualizarAsync(_mapper.Map<Endereco>(clienteDTO));
        }

        public async Task<bool> ValidarDuplicidadeDeEmailAsync(string email, long id)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("E-mail informado inválido");

            var resultado = await _enderecoClienteRepository.ValidarDuplicidadeDeEmailAsync(email, id);
            return resultado;
        }

        private void ValidaEndereco(EnderecoDTO endereco)
        {
            if (endereco == null)
                throw new Exception("Valores de entrada inválidos");

            if (string.IsNullOrWhiteSpace(endereco.Logradouro))
                throw new Exception("Logradouro não informado");

            if (string.IsNullOrWhiteSpace(endereco.Numero))
                throw new Exception("Número não informado");

            if (string.IsNullOrWhiteSpace(endereco.Bairro))
                throw new Exception("Bairro não informada");

            if (string.IsNullOrWhiteSpace(endereco.Cidade))
                throw new Exception("Cidade não informada");

            if (endereco.Uf < 1)
                throw new Exception("UF não informada");
        }

    }
}
