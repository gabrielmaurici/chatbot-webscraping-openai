namespace WebScraping.ChatGpt.Application.Interfaces;

public interface INextMatchApplication
{
    Task<string> Get(string team);
}