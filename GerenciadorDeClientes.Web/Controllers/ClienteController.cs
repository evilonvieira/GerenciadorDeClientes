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
    [Route("clientes")]
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IIntegradorWebApi _integradorWebApi;
        private readonly AppSettings _appSettings;

        public ClienteController(ILogger<ClienteController> logger, IHttpClientFactory clientFactory, IIntegradorWebApi integradorWebApi, AppSettings appSettings)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _integradorWebApi = integradorWebApi;
            _appSettings = appSettings;
        }

        [Authorize, HttpGet("")]
        public async Task<IActionResult> Inicio()
        {
            return RedirectToAction("Index", "Cliente", new
            {
                pagina = 1,
                registros = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas
            });
        }

        [Authorize, HttpGet("{pagina}/{registros}")]
        public async Task<IActionResult> Index(int pagina, int registros)
        {
            try
            {
                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;
                var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
                var met = _appSettings.webApiGerenciadorDeClientes?.metodoClientesListar ?? "";
                var token = Request.Cookies["AuthToken"];
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token não encontrado no cookie.");
                }

                met += $"/{pagina}/{registros}";

                var resultadoLogin = await _integradorWebApi.GetAsync<PaginacaoLista<ClienteDTO>>(url, met, token);
                if (resultadoLogin == null)
                    throw new Exception("Falha ao estabelecer contato a api");

                if (!resultadoLogin.Sucesso)
                    if (string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
                        throw new Exception("Mensagem de erro vazia");
                    else
                        throw new Exception(resultadoLogin.MensagemDeErro);

                if (resultadoLogin.Retorno == null)
                    throw new Exception("Nenhum Token foi retornado da api");


                return View(resultadoLogin.Retorno);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
            }

            return View();
        }

        [Authorize, HttpGet("excluir/{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            try
            {
                var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
                var met = _appSettings.webApiGerenciadorDeClientes?.metodoClientesPesquisarPorId ?? "";
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


                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;

                return View(resultadoLogin.Retorno);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
            }

            return View();
        }

        [Authorize, HttpPost("excluir")]
        public async Task<IActionResult> ExcluirCliente([FromForm] long id)
        {
            try
            {
                var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
                var met = _appSettings.webApiGerenciadorDeClientes?.metodoExcluirClientePorId ?? "";
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
                ViewBag.SucessoNaExclusao = true;

                return View("Excluir");
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
            }

            return View();
        }


        [Authorize, HttpGet("cadastrar")]
        public async Task<IActionResult> Cadastrar(long id)
        {
            try
            {

                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;
                ViewBag.ModoCadastro = true;

                return View("Manter", new ClienteDTO { Id = (int)id });
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
            }

            return View();
        }


        [Authorize, HttpGet("editar/{id}")]
        public async Task<IActionResult> Editar([FromRoute] long id)
        {
            try
            {
                ViewBag.ModoCadastro = false;
                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;


                var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
                var met = _appSettings.webApiGerenciadorDeClientes?.metodoClientesPesquisarPorId ?? "";
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



                return View("Manter", resultadoLogin.Retorno);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
            }

            return View();
        }

        [Authorize, HttpPost("manter")]
        public async Task<IActionResult> ManterCliente([Bind] ClienteDTO model)
        {
            try
            {

                ViewBag.ModoCadastro = model.IsNovo > 0;
                ViewBag.RegistrosPorPagina = _appSettings.webApiGerenciadorDeClientes.registrosPorPaginas;


                model.Validate();


                await ValidarDuplicidadeDeEmail(model.Id, model.Email);


                //valida e-mail


                
                var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
                var met = _appSettings.webApiGerenciadorDeClientes?.metodoSalvar ?? "";
                var token = Request.Cookies["AuthToken"];
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token não encontrado no cookie.");
                }

                var resultadoLogin = await _integradorWebApi.PostAsync<Cliente>(url, met, token, model);
                if (resultadoLogin == null)
                    throw new Exception("Falha ao estabelecer contato a api");

                if (!resultadoLogin.Sucesso)
                    if (string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
                        throw new Exception("Mensagem de erro vazia");
                    else
                        throw new Exception(resultadoLogin.MensagemDeErro);

                if(resultadoLogin.Sucesso && !string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
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

            return View("Manter", model);
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

    public class ClienteModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
