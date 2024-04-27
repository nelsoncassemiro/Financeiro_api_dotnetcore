using Domain.Interfaces.Generics;
using Domain.Interfaces.ICategoria;
using Domain.Interfaces.IDespesa;
using Domain.Interfaces.IServicos;
using Domain.Interfaces.ISistemaFinanceiro;
using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Domain.Servicos;
using Entities.Entidades;
using Infra.Configuracao;
using Infra.Repositorio;
using Infra.Repositorio.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextBase>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ContextBase>();

//INTERFACE E REPOSITORIO
builder.Services.AddSingleton(typeof(InterfaceGeneric<>), typeof(RepositoryGenerics<>));
builder.Services.AddSingleton<ICategoria, RepositorioCategoria>();
builder.Services.AddSingleton<IDespesa, RepositorioDespesa>();
builder.Services.AddSingleton<ISistemaFinanceiro, RepositorioSistemaFinanceiro>();
builder.Services.AddSingleton<IUsuarioSistemaFinanceiro, RepositorioUsuarioSistemaFinanceiro>();

//SERVICO DOMINIO
builder.Services.AddSingleton<ICategoriaServico, CategoriaServico>();
builder.Services.AddSingleton<IDespesaServico, DespesaServico>();
builder.Services.AddSingleton<ISistemaFinanceiroServico, SistemaFinanceiroServico>();
builder.Services.AddSingleton<IUsuarioSistemaFinanceiroServico, UsuarioSistemaFinanceiroServico>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Teste.Security.Bearer",
            ValidAudience = "Teste.Security.Bearer",
            IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
        };

        option.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                return Task.CompletedTask;
            }
        };
    });


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
