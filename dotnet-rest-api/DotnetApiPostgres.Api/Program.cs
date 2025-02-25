using DotnetApiPostgres.Api;
using Microsoft.EntityFrameworkCore;
using DotnetApiPostgres.Api.Services;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotNetEnv.Env.Load();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(GetConnectionString()));

builder.Services.AddTransient<IPersonService, PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

static string GetConnectionString()
{
    var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
    var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
    var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "dotnet-api";
    var username = Environment.GetEnvironmentVariable("DB_USER") ?? "";
    var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

    return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
}
