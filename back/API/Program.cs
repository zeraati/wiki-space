global using Common;
global using FastEndpoints;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

using API;
using Common.HttpClient;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddFastEndpoints()
    .AddSwaggerConfig(true)
    .AddHttpContextAccessor()
    .AddDbConfig<AppDbContext>(AppConfig.Connection)
    .AddAuthConfig(AppConfig.Issuer, AppConfig.Audience)
    .AddHttpClient<HttpClientService>();


var app = builder.Build();
app.UseAuthentication().UseAuthorization()
   .UseDefaultExceptionHandler(useGenericReason: app.Environment.IsDevelopment()==false)
   .UseFastEndpoints(x => { x.Endpoints.RoutePrefix = "api"; })
   .UseSwaggerGen();
app.Run();
