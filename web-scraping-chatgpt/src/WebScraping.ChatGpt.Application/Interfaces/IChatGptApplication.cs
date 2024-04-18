namespace WebScraping.ChatGpt.Application.Interfaces;

public interface IChatGptApplication
{
    Task<string> AskQuestion(string ask);
}