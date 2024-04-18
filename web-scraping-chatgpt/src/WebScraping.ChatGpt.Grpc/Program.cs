using WebScraping.ChatGpt.Application.Interfaces;
using WebScraping.ChatGpt.Application.Services;
using WebScraping.ChatGpt.Domain.Services;
using WebScraping.ChatGpt.Grpc.Services;
using WebScraping.ChatGpt.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var teste = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
Console.WriteLine("Teste " + teste);
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IChatGptService, ChatGptService>();
builder.Services.AddScoped<IChatGptApplication, ChatGptApplication>();
builder.Services.AddScoped<ILastMatchApplication, LastMatchApplication>();
builder.Services.AddScoped<INextMatchApplication, NextMatchApplication>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<WebScrapingGrpcService>();
app.MapGrpcService<ChatGptGrpcService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();