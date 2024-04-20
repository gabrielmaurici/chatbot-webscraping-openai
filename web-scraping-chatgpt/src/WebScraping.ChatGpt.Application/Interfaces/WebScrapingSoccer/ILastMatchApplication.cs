namespace WebScraping.ChatGpt.Application.Interfaces.WebScrapingSoccer;

public interface ILastMatchApplication
{
    Task<string> Get(string team);
}