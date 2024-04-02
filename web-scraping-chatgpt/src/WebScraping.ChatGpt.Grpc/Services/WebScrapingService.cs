using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using WebScraping.ChatGpt.Infrastructure;

namespace WebScraping.ChatGpt.Grpc.Services;

public class WebScrapingService : WebScraping.WebScrapingBase
{
    private readonly ILogger<WebScrapingService> _logger;
    public WebScrapingService(ILogger<WebScrapingService> logger)
    {
        _logger = logger;
    }

    public override async Task<LastMatchReply> GetLastMatch(Empty request, ServerCallContext context)
    {
        var teste = new WebScrapingLastMatchService();
        var response = await teste.ExecuteScraping();
        
        return new LastMatchReply {
            LastMatch = response.ToString()
        };
    }
}
