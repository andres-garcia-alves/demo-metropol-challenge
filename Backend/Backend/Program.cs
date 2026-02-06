using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Scalar.AspNetCore;
using Serilog;
using Backend.BusinessLogic;
using Backend.BusinessLogic.Interfaces;
using Backend.BusinessLogic.Validators;
using Backend.DataAccess;
using Backend.DataAccess.Interfaces;
using Backend.DTOs;

var builder = WebApplication.CreateBuilder(args);

// configuración de Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// configuración de SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=metropol.db"));

// configuración de AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// configuración de FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PersonaValidator>();

// registro de los [Entidad]Repository y los [Entidad]Logic
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPersonaLogic, PersonaLogic>();

// permitir CORS para el Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// habilitar OpenApi y Scalar en todos los entornos para la demo
app.MapOpenApi();
app.MapScalarApiReference();

// redirección predeterminada a la documentación
app.MapGet("/", () => Results.Redirect("/scalar/v1"));

// asegurar que la DB esté creada
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
