using WebScraping.ChatGpt.Domain.Models.WebScraping;

namespace WebScraping.ChatGpt.Domain.Services.WebScraping;

public interface IWebScrapingService<TResponse> where TResponse : MatchBaseModel 
{
    Task<TResponse> ExecuteScraping();
}