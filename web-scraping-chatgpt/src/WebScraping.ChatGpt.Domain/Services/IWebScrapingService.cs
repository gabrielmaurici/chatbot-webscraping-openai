using WebScraping.ChatGpt.Domain.Models;

namespace WebScraping.ChatGpt.Domain.Services;

public interface IWebScrapingService<TResponse> where TResponse : MatchBaseModel 
{
    Task<TResponse> ExecuteScraping();
}