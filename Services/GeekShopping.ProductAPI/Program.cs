using AutoMapper;
using GeekShopping.ProductAPI.Config;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddSwaggerGen(options =>
{
    // Configurações básicas
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Product API",
        Version = "v1",
        Description = "Documentação da API Product"
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

app.UseAuthorization();

app.MapControllers();

app.Run();
