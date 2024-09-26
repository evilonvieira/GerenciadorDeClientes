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

namespace GerenciadorDeClientes.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IIntegradorWebApi _integradorWebApi;
        private readonly AppSettings _appSettings;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, IIntegradorWebApi integradorWebApi, AppSettings appSettings)
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
            }) ;
        }


    }
}
