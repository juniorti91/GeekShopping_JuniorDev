using AutoMapper;
using GeekShopping.ProductAPI.Config;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuração com o banco de dados
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.
            UseMySql(connection, 
                    new MySqlServerVersion(
                        new Version(8, 0, 24))));

// Configuração do AutoMapper
var mappingConfig = MappingConfig.RegisterMaps();
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// COnfigurando a Injeção de Dependência
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Adicionando configurações de Segurança de Autenticação
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", option => 
    {
        option.Authority = "https://localhost:4435/";
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

// Adicionando configurações de Segurança de Autorização
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "geek_shopping");
    });
});

// Configuração do Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product API",
        Version = "v1",
        Description = "Documentação da API Product"
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Entrer 'Bearer' [espaço] e o token JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Configurar Kestrel para escutar na porta 5047
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5047); // Permite conexões de qualquer IP na porta 5047
});

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalAndExternal",
        policyBuilder => policyBuilder
            .WithOrigins(
                "http://localhost:5047", // Origem do Frontend em desenvolvimento
                "http://191.252.103.130:8081" // Origem da API em Produção
            )
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar o uso do Swagger em Produção
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
