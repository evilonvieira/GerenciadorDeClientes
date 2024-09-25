using GerenciadorDeClientes.Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeClientes.WebApi.Domain.Services;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Services;
using GerenciadorDeClientes.WebApi.Application;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories;
using GerenciadorDeClientes.Infra.Data;
using GerenciadorDeClientes.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Configuração do DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//AppSettings
var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);


//IoC
builder.Services.AddSingleton(appSettings);
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
//
builder.Services.AddScoped < IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();


// Adicionar AutoMapper aos serviços
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Configurar Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],     // Definir no appsettings.json
        ValidAudience = builder.Configuration["Jwt:Audience"], // Definir no appsettings.json
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
