using GerenciadorDeClientes.WebApi.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GerenciadorDeClientes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;

        public AuthController(ILogger<AuthController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {

            }
            catch (Exception error)
            {

                throw;
            }
            if (IsValidUser(login)) // Valida as credenciais do usuário
            {
                var token = GenerateJwtToken(login);
                return Ok(new { token });
            }
            return Unauthorized();
        }
        private string GenerateJwtToken(LoginDTO login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                //new Claim(ClaimTypes.Name, login.Username),
                new Claim("CustomClaim", "CustomValue") // Claim personalizada
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool IsValidUser(LoginDTO login)
        {
            // Lógica para validar o usuário (pode ser banco de dados ou hardcoded)
            return login.Email == "user" && login.Senha == "password";
        }
    }

    
}
