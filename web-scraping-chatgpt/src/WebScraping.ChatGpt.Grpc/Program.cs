using WebScraping.ChatGpt.Grpc.Services;
using WebScraping.ChatGpt.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainDependeces(builder.Configuration);
builder.Services.AddApllicationDependeces();
builder.Services.AddMemoryCache();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<WebScrapingGrpcService>();
app.MapGrpcService<ChatGptGrpcService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();