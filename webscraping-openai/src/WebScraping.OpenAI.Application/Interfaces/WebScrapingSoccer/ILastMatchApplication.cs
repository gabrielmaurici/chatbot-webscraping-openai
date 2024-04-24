namespace WebScraping.OpenAI.Application.Interfaces.WebScrapingSoccer;

public interface ILastMatchApplication
{
    Task<string> Get(string team);
}