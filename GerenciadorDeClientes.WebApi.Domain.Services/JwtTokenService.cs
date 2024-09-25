using GerenciadorDeClientes.WebApi.Application;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Services;
using GerenciadorDeClientes.WebApi.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.WebApi.Domain.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        public readonly AppSettings _appSettings;

        public JwtTokenService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string GerarBearerToken(Usuario usuario)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.jwt?.Key ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("CustomClaim", "CustomValue") // Claim personalizada
            };

            var token = new JwtSecurityToken(
                issuer: _appSettings.jwt?.Issuer, // _config["Jwt:Issuer"],
                audience: _appSettings.jwt?.Audience, // _config["Jwt:Audience"],
                claims: claims,
                //expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_appSettings.jwt?.ExpireMinutes)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
