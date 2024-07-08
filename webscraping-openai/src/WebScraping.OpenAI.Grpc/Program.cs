using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using WebScraping.OpenAI.Application.Interfaces.OpenAI;
using WebScraping.OpenAI.Application.Interfaces.WebScrapingSoccer;
using WebScraping.OpenAI.Grpc.Services;
using WebScraping.OpenAI.IoC;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddDomainDependeces(builder.Configuration);
builder.Services.AddApllicationDependeces();
builder.Services.AddMemoryCache();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<WebScrapingGrpcService>();
app.MapGrpcService<ChatGptGrpcService>();
app.MapGrpcService<ImageDalleGrpcService>();

app.Run();