using Grpc.Core;
using WebScraping.ChatGpt.Infrastructure.Services.LastMatchServices.AbstractService;
using WebScraping.ChatGpt.Infrastructure.Services.LastMatchServices.ConcretServices;
using WebScraping.ChatGpt.Infrastructure.Services.NextMatchServices.AbstractService;
using WebScraping.ChatGpt.Infrastructure.Services.NextMatchServices.ConcretServices;

namespace WebScraping.ChatGpt.Grpc.Services;

public class WebScrapingService : WebScraping.WebScrapingBase
{
    public override async Task<LastMatchReply> GetLastMatch(LastMatchRequest request, ServerCallContext context)
    {
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

    public override async Task<NextMatchReply> GetNextMatch(NextMatchRequest request, ServerCallContext context)
    {
        WebScrapingNextMatchService webScrapingService = request.Team switch {
            "Fluminense" => new FluminenseNextMatchService(),
            "Flamengo" => new FlamengoNextMatchService(),
            "Brusque" => new BrusqueNextMatchService(),
            _ => throw new Exception("Nenhum time correspondente")
        };

        var response = await webScrapingService.ExecuteScraping();
        return new NextMatchReply {
            NextMatch = response.ToString()
        };
    }
}
