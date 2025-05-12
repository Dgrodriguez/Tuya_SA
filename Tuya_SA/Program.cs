using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tuya_SA.Models;
using Tuya_SA.Domain;
using Tuya_SA.Aplication;
using Tuya_SA.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

// Probar conexión ANTES de ejecutar `app.Run()`
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

if (dbContext.ProbarConexion())
    logger.LogInformation(" Conexión exitosa con SQL Server!");
else
    logger.LogError(" Error en la conexión con SQL Server.");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }