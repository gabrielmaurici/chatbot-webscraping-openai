namespace WebScraping.ChatGpt.Application.Interfaces.WebScrapingSoccer;

public interface INextMatchApplication
{
    Task<string> Get(string team);
}