using Grpc.Core;
using WebScraping.ChatGpt.Infrastructure;
using WebScraping.ChatGpt.Infrastructure.Services;

namespace WebScraping.ChatGpt.Grpc.Services;

public class WebScrapingService : WebScraping.WebScrapingBase
{
    public override async Task<LastMatchReply> GetLastMatch(LastMatchRequest request, ServerCallContext context)
    {
        Console.WriteLine("Alguma coisa: " + request.Team);
        WebScrapingLastMatchService webScrapingService = request.Team switch {
            "Fluminense" => new FluminenseLastMatchService(),
            "Flamengo" => new FlamengoLastMatchService(),
            "Brusque" => new BrusqueLastMatchService(),
            _ => throw new Exception("Nenhum time correspondente")
        };

        var response = await webScrapingService.ExecuteScraping();
        
        return new LastMatchReply {
            LastMatch = response.ToString()
        };
    }
}
