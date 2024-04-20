using Microsoft.Extensions.Caching.Memory;
using WebScraping.ChatGpt.Application.Interfaces;
using WebScraping.ChatGpt.Application.Interfaces.WebScrapingSoccer;
using WebScraping.ChatGpt.Domain.Models.WebScraping;
using WebScraping.ChatGpt.Infrastructure.Services.NextMatchServices.AbstractService;
using WebScraping.ChatGpt.Infrastructure.Services.NextMatchServices.ConcretServices;

namespace WebScraping.ChatGpt.Application.Services.WebScrapingSoccer;

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