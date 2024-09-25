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

        public ClienteController(ILogger<ClienteController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [Authorize, HttpGet("list")]
        public async Task<IList<ClienteModel>> ListarCliente() //[FromBody] LoginModel login)
        {
            return await Task.Run< IList < ClienteModel >>(() => new List<ClienteModel> { 
                new ClienteModel{Username = "a", Password = "b"},
                new ClienteModel{Username = "c", Password = "d"},
                new ClienteModel{Username = "e", Password = "f"},
            });
        }
    }

    public class ClienteModel {
        public required string Username { get; set; } 
        public required string Password { get; set; } 
    }
}
