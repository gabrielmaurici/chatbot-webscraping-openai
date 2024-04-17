using WebScraping.ChatGpt.Application.Interfaces;
using WebScraping.ChatGpt.Application.Services;
using WebScraping.ChatGpt.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ILastMatchApplication, LastMatchApplication>();
builder.Services.AddScoped<INextMatchApplication, NextMatchApplication>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<WebScrapingService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();