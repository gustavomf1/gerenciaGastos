using Dominio.Dtos;
using FluentValidation;
using gerenciaGastos.Mapping;
using gerenciaGastos.Validation;
using InfraEstrutura.Data;
using InfraEstrutura.Repositorio;
using Interface.Repositorio;
using Interface.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira 'Bearer' + espaço + token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// <-- ADICIONADO AQUI (Definição da Política de CORS)
var myPolicy = "AllowReactApp";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myPolicy,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000") // A URL do seu frontend
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});
// --- Fim da adição de CORS ---


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

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);



builder.Services.AddDbContext<GastosContexto>
    (p => p.UseSqlServer(
        builder.Configuration
        .GetConnectionString("default")));

builder.Services.AddAutoMapper(
    p => p.AddProfile<MappingProfile>());

builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IOrcamentoService, OrcamentoService>();
builder.Services.AddScoped<IOrcamentoRepositorio, OrcamentoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITransacaoRepositorio, TransacaoRepositorio>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();

builder.Services.AddScoped<IValidator<CategoriaDto>, CategoriaValidation>();
builder.Services.AddScoped<IValidator<OrcamentoDto>, OrcamentoValidation>();
builder.Services.AddScoped<IValidator<TransacaoCreateDto>, TransacaoValidation>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// <-- ADICIONADO AQUI (Ativação do Middleware de CORS)
// Esta linha deve vir ANTES de UseAuthentication e UseAuthorization
app.UseCors(myPolicy);
// --- Fim da ativação de CORS ---

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();