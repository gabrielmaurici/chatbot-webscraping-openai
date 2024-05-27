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
        listenOptions.Protocols = HttpProtocols.Http1;
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

app.MapGet("api/last-match/{team}", async (string team, ILastMatchApplication lastMatchApplication) =>
{
    return Results.Ok(await lastMatchApplication.Get(team));
});

app.MapGet("api/next-match/{team}", async (string team, INextMatchApplication nextMatchApplication) =>
{
    return Results.Ok(await nextMatchApplication.Get(team));
});

app.MapGet("api/chatgpt/{ask}", async (string ask, IChatGptApllication chatGptApllication) =>
{
    return Results.Ok(await chatGptApllication.AskQuestion(ask));
});

app.MapGet("api/image-dall-e/{imageDescription}", async (string imageDescription, IImageDalleApplication imageDalleApplication) =>
{
    return Results.Ok(await imageDalleApplication.GenerateImage(imageDescription));
});

app.Run();