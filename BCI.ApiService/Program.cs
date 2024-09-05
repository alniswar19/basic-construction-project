using BCI.ApiService;
using BCI.ApiService.Middleware;
using BCI.Domain.Config;
using BCI.Domain.Entities.Mapping;
using BCI.Domain.Repositories;
using BCI.Infrastructure.Persistances;
using Dapper.FluentMap;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var env = Environments.Development; // Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", true, true);

builder.AddSqlServerClient("BCIDB");

builder.Services.Configure<AppConfig>(
    builder.Configuration.GetSection(
        key: nameof(AppConfig)));

builder.Services.AddSingleton<IDbConnectionFactory, DbFactory>();
builder.Services.AddScoped<ConstructionProjectRepository>();

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapConstructionApi();
FluentMapper.Initialize(config =>
{
    config.AddMap(new UserMap());
});

app.UseMiddleware<ExceptionMiddleware>();
app.MapDefaultEndpoints();

app.Run();