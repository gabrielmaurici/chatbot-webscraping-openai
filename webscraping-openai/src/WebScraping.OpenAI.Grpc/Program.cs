using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using WebScraping.OpenAI.Grpc.Services;
using WebScraping.OpenAI.IoC;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
    });
});

Console.WriteLine("teste: " + builder.Configuration.GetValue<string>("OPENAI_API_KEY"));

builder.Services.AddDomainDependeces(builder.Configuration);
builder.Services.AddApllicationDependeces();
builder.Services.AddMemoryCache();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<WebScrapingGrpcService>();
app.MapGrpcService<ChatGptGrpcService>();
app.MapGrpcService<ImageDalleGrpcService>();
app.MapGet("/", () => "Testando Deploy Render");


app.Run();