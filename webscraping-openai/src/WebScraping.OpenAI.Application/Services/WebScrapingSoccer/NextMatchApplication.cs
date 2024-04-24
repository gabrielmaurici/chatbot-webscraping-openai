using Microsoft.Extensions.Caching.Memory;
using WebScraping.OpenAI.Application.Interfaces.WebScrapingSoccer;
using WebScraping.OpenAI.Domain.Models.WebScraping;
using WebScraping.OpenAI.Infrastructure.Services.NextMatchServices.AbstractService;
using WebScraping.OpenAI.Infrastructure.Services.NextMatchServices.ConcretServices;

namespace WebScraping.OpenAI.Application.Services.WebScrapingSoccer;

public class NextMatchApplication(IMemoryCache memoryCache) : INextMatchApplication
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<string> Get(string team)
    {
        var cacheKey = "nextMatch" + team;

        var nextMatchCache = _memoryCache.Get<NextMatchModel>(cacheKey);
        if(nextMatchCache != null)
            return nextMatchCache.ToString();

        WebScrapingNextMatchService webScrapingService = team switch {
            "Fluminense" => new FluminenseNextMatchService(),
            "Flamengo" => new FlamengoNextMatchService(),
            "Brusque" => new BrusqueNextMatchService(),
            _ => throw new Exception("Nenhum time correspondente")
        };

        var response = await webScrapingService.ExecuteScraping();
        _memoryCache.Set(cacheKey, response, TimeSpan.FromMinutes(15));

        return response.ToString();
    }
}