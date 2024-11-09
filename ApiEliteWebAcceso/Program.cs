using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.Services;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configura la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefatultConnectionString");


// Registrar IDbConnection en el contenedor de dependencias
builder.Services.AddSingleton<IDbConnection>(provider => new SqlConnection(connectionString));

// Registra el repositorio
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// Registra servicios
builder.Services.AddScoped<IAuthService, AuthService>();

#region CORS
builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy(name: "CorsPolicy", builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
    }
    else
    {
        options.AddPolicy(name: "CorsPolicy", builder => builder
            .WithOrigins("http://www.elitesoftweb.com")  // Configuración específica para producción
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
    }
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

app.Run();
