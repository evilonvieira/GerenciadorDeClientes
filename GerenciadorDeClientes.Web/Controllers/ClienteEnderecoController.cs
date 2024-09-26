using GerenciadorDeClientes.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;
using GerenciadorDeClientes.Web.Application.Interfaces;
using GerenciadorDeClientes.Infra.CrossCutting;
using GerenciadorDeClientes.Infra.Core.Util;
using GerenciadorDeClientes.Web.Application.DTOs;
using Microsoft.Win32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using GerenciadorDeClientes.Infra.CrossCutting.Extensions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using GerenciadorDeClientes.WebApi.Domain.Entities;

namespace GerenciadorDeClientes.Web.Controllers
{
    [Route("clientes/endereco")]
    public class ClienteEnderecoController : Controller
    {
        private readonly ILogger<ClienteEnderecoController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IIntegradorWebApi _integradorWebApi;
        private readonly AppSettings _appSettings;

        public ClienteEnderecoController(ILogger<ClienteEnderecoController> logger, IHttpClientFactory clientFactory, IIntegradorWebApi integradorWebApi, AppSettings appSettings)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _integradorWebApi = integradorWebApi;
            _appSettings = appSettings;
        }


        [Authorize, HttpGet("{id}")]
        public async Task<IActionResult> Enderecos([FromRoute] long id)
        {
            try
            {
                ViewBag.ModoCadastro = false;
                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;


                var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
                var met = _appSettings.webApiGerenciadorDeClientes?.metodoClientesPesquisarComEnderecosPorId ?? "";
                var token = Request.Cookies["AuthToken"];
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token não encontrado no cookie.");
                }

                met += $"/{id}";

                var resultadoLogin = await _integradorWebApi.GetAsync<ClienteDTO>(url, met, token);
                if (resultadoLogin == null)
                    throw new Exception("Falha ao estabelecer contato a api");

                if (!resultadoLogin.Sucesso)
                    if (string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
                        throw new Exception("Mensagem de erro vazia");
                    else
                        throw new Exception(resultadoLogin.MensagemDeErro);

                if (resultadoLogin.Retorno == null)
                    throw new Exception("Nenhum Token foi retornado da api");



                return View("../Endereco/Enderecos", resultadoLogin.Retorno);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
            }

            return View();
        }



        private async Task<ClienteDTO> PesquisaClienteDoEnderecoAsync(long id)
        {
            var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
            var met = _appSettings.webApiGerenciadorDeClientes?.metodoClientesPesquisarComEnderecosPorId ?? "";
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token não encontrado no cookie.");
            }

            met += $"/{id}";

            var resultadoLogin = await _integradorWebApi.GetAsync<ClienteDTO>(url, met, token);
            if (resultadoLogin == null)
                throw new Exception("Falha ao estabelecer contato a api");

            if (!resultadoLogin.Sucesso)
                if (string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
                    throw new Exception("Mensagem de erro vazia");
                else
                    throw new Exception(resultadoLogin.MensagemDeErro);

            if (resultadoLogin.Retorno == null)
                throw new Exception("Nenhum Token foi retornado da api");

            return resultadoLogin.Retorno;
        }




        [Authorize, HttpGet("{id}/adicionar")]
        public async Task<IActionResult> AdicionarEnderecos([FromRoute] long id)
        {
            try
            {
                ViewBag.ModoCadastro = false;
                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;

                var cliente = await PesquisaClienteDoEnderecoAsync(id);


                return View("../Endereco/Manter", new EnderecoViewModel
                {
                    Cliente = cliente,
                    Endereco = new EnderecoDTO { Id = 0 }
                });
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
            }

            return View();
        }

        [Authorize, HttpGet("editar/{id}/{idEndereco}")]
        public async Task<IActionResult> AtualizarEnderecos([FromRoute] long id, long idEndereco)
        {
            try
            {
                ViewBag.ModoCadastro = false;
                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;


                var cliente = await PesquisaClienteDoEnderecoAsync(id);

                var enderecoEscolhido = cliente.Enderecos.FirstOrDefault(x => x.Id == idEndereco);
                if (enderecoEscolhido == null)
                    throw new Exception("Endereço não encontrado");


                return View("../Endereco/Manter", new EnderecoViewModel
                {
                    Cliente = cliente,
                    Endereco = enderecoEscolhido
                });
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
            }

            return View();
        }

        [Authorize, HttpPost("gerenciar")]
        public async Task<IActionResult> ManterEnderecoCliente([Bind] EnderecoDTO model)
        {
            ClienteDTO cliente = null;
            try
            {

                ViewBag.ModoCadastro = model.Id == 0;
                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;

                cliente = await PesquisaClienteDoEnderecoAsync(model.IdCliente);



                model.Validate();




                //valida e-mail



                var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
                var met = _appSettings.webApiGerenciadorDeClientes?.metodoSalvarEndereco ?? "";
                var token = Request.Cookies["AuthToken"];
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token não encontrado no cookie.");
                }

                var resultadoLogin = await _integradorWebApi.PostAsync<Endereco>(url, met, token, model);
                if (resultadoLogin == null)
                    throw new Exception("Falha ao estabelecer contato a api");

                if (!resultadoLogin.Sucesso)
                    if (string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
                        throw new Exception("Mensagem de erro vazia");
                    else
                        throw new Exception(resultadoLogin.MensagemDeErro);

                if (resultadoLogin.Sucesso && !string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
                    throw new ArgumentException(resultadoLogin.MensagemDeErro);


                ViewBag.SucessoNoProcessamento = true;



            }
            catch (ArgumentException warning)
            {
                ModelState.AddModelError("", warning.Message);
            }
            catch (Exception error)
            {
                //loga o erro técnico
                _logger.LogError(error.Message);

                //retorna erro tratado para o usuário
                ModelState.AddModelError("", error.ErroTratado());
            }

            return View("../Endereco/Manter", new EnderecoViewModel
            {
                Cliente = cliente,
                Endereco = model
            });
        }


        [Authorize, HttpGet("excluir/{id}/{idEndereco}")]
        public async Task<IActionResult> Excluir([FromRoute] long id, long idEndereco)
        {
            ClienteDTO? cliente = null;
            EnderecoDTO? enderecoEscolhido = null;
            try
            {
                ViewBag.ModoCadastro = false;
                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;


                cliente = await PesquisaClienteDoEnderecoAsync(id);

                enderecoEscolhido = cliente.Enderecos.FirstOrDefault(x => x.Id == idEndereco);
                if (enderecoEscolhido == null)
                    throw new Exception("Endereço não encontrado");

            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
            }

            return View("../Endereco/Excluir", new EnderecoViewModel
            {
                Cliente = cliente ?? new ClienteDTO(),
                Endereco = enderecoEscolhido ?? new EnderecoDTO()
            });
        }

        [Authorize, HttpPost("excluir-endereco")]
        public async Task<IActionResult> ExcluirEnderecoCliente([FromForm] long id, long idCliente)
        {
            ClienteDTO? cliente = null;
            EnderecoDTO? enderecoEscolhido = null;
            try
            {

                cliente = await PesquisaClienteDoEnderecoAsync(idCliente);
                enderecoEscolhido = cliente.Enderecos.FirstOrDefault(x => x.Id == id);
                if (enderecoEscolhido == null)
                    throw new Exception("Endereço não encontrado");


                

                var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
                var met = _appSettings.webApiGerenciadorDeClientes?.metodoExcluirEnderecoPorId ?? "";
                var token = Request.Cookies["AuthToken"];
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token não encontrado no cookie.");
                }

                met += $"/{id}";

                var resultadoLogin = await _integradorWebApi.DeleteAsync(url, met, token, id);
                if (resultadoLogin == null)
                    throw new Exception("Falha ao estabelecer contato a api");

                if (!resultadoLogin.Sucesso)
                    if (string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
                        throw new Exception("Mensagem de erro vazia");
                    else
                        throw new Exception(resultadoLogin.MensagemDeErro);
                

                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;
                ViewBag.SucessoNoProcessamento = true;
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;

            }

            return View("../Endereco/Excluir", new EnderecoViewModel
            {
                Cliente = cliente ?? new ClienteDTO(),
                Endereco = enderecoEscolhido ?? new EnderecoDTO()
            });
        }

















        private async Task ValidarDuplicidadeDeEmail(long id, string email)
        {

            var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
            var met = _appSettings.webApiGerenciadorDeClientes?.metodoValidarDuplicidadeEmail ?? "";
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token não encontrado no cookie.");
            }

            met += $"/{id}/{email}";

            var resultadoLogin = await _integradorWebApi.GetAsync<bool>(url, met, token);
            if (resultadoLogin == null)
                throw new Exception("Falha ao estabelecer contato a api");

            if (!resultadoLogin.Sucesso)
                if (string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
                    throw new Exception("Mensagem de erro vazia");
                else
                    throw new Exception(resultadoLogin.MensagemDeErro);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
