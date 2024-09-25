using GerenciadorDeClientes.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;

namespace GerenciadorDeClientes.Web.Controllers
{
    
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public ClienteController(ILogger<ClienteController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize, HttpGet("privacy")]
        public async Task<IActionResult> Privacy()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token não encontrado no cookie.");
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("https://localhost:7229/Cliente/list");
            IList<ClienteModel> Clientes = new List<ClienteModel>();
            if (response.IsSuccessStatusCode)
            {
                Clientes = await response.Content.ReadFromJsonAsync<List<ClienteModel>>() ;
            }



            return View(Clientes);
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
