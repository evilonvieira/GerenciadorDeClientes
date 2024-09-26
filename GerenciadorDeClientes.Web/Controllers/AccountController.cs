using GerenciadorDeClientes.Infra.CrossCutting;
using GerenciadorDeClientes.Infra.CrossCutting.Extensions;
using GerenciadorDeClientes.Web.Application.Interfaces;
using GerenciadorDeClientes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace GerenciadorDeClientes.Web.Controllers
{

    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IIntegradorWebApi _integradorWebApi;
        private readonly AppSettings _appSettings;
        public AccountController(ILogger<AccountController> logger, IHttpClientFactory clientFactory, IIntegradorWebApi integradorWebApi, AppSettings appSettings)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _integradorWebApi = integradorWebApi;
            _appSettings = appSettings;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                model?.Validate();

                //comunicaçao com a WebApiGerenciadorDeClientes
                var url = _appSettings.webApiGerenciadorDeClientes?.url ?? "";
                var met = _appSettings.webApiGerenciadorDeClientes?.metodoAuth ?? "";
                var resultadoLogin = await _integradorWebApi.PostAsync<TokenResponse>(url, met, body: model);
                if (resultadoLogin == null)
                    throw new Exception("Falha ao estabelecer contato a api");

                if(!resultadoLogin.Sucesso)
                    throw new Exception(resultadoLogin.MensagemDeErro);

                if(resultadoLogin.Sucesso && !string.IsNullOrWhiteSpace(resultadoLogin.MensagemDeErro))
                    throw new ArgumentException(resultadoLogin.MensagemDeErro);

                if (resultadoLogin.Retorno == null || string.IsNullOrWhiteSpace(resultadoLogin.Retorno.Token))
                    throw new Exception("Nenhum Token foi retornado da api");


                //criando cookie de autenticação
                // Armazenar o token em um cookie (ou outro local, como localStorage)
                HttpContext.Response.Cookies.Append("AuthToken", resultadoLogin.Retorno.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict, // Para evitar CSRF
                    Expires = DateTime.UtcNow.AddMinutes(30)
                });

                return RedirectToAction("Index", "Cliente", new { pagina = 1, registros = _appSettings.webApiGerenciadorDeClientes?.registrosPorPaginas ?? 10 });

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


            return View(model);

            /*
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7229/auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

                if (result != null && !string.IsNullOrWhiteSpace(result.Token))
                {
                    // Armazenar o token em um cookie (ou outro local, como localStorage)
                    HttpContext.Response.Cookies.Append("AuthToken", result.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict, // Para evitar CSRF
                        Expires = DateTime.UtcNow.AddMinutes(30)
                    });

                    return RedirectToAction("Index", "Cliente");
                }
            }

            ModelState.AddModelError("", "Login inválido.");
            return View(model);
            */
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Remover o cookie do token JWT
            Response.Cookies.Delete("AuthToken");

            return RedirectToAction("Login", "Account");
        }
    }

    /*
    public class TokenResponse
    {
        public required string Token { get; set; }
    }

    public class LoginModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
    */
}
