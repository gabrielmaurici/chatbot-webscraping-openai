using WebScraping.OpenAI.Domain.Models.WebScraping;

namespace WebScraping.OpenAI.Domain.Services.WebScraping;

public interface IWebScrapingService<TResponse> where TResponse : MatchBaseModel 
{
    Task<TResponse> ExecuteScraping();
}