namespace WebScraping.ChatGpt.Application.Interfaces;

public interface ILastMatchApplication
{
    Task<string> Get(string team);
}