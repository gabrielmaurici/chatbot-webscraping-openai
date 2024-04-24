namespace WebScraping.OpenAI.Application.Interfaces.WebScrapingSoccer;

public interface INextMatchApplication
{
    Task<string> Get(string team);
}