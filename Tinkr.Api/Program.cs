
using Tinkr.Api.Authorization;
using Tinkr.Api.Cors;
using Tinkr.Api.Data;
using Tinkr.Api.ErrorHandling;
using Tinkr.Api.Middleware;
using Tinkr.Api.TinkrEndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepository(builder.Configuration);

builder.Services.AddAuthentication()
                .AddJwtBearer("Auth0");

builder.Services.AddProjectAuthorization();
builder.Services.AddHttpLogging();

builder.Services.AddProjectStoreCors(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());
app.UseMiddleware<RequestTimingMiddleware>();

await app.Services.InitializeDbAsync();

app.UseHttpLogging();
app.MapProjectsEndpoints();
app.UseCors();

app.Run();
