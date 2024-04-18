namespace WebScraping.ChatGpt.Domain.Services;

public interface IChatGptService
{
    Task<string> AskQuestion(string ask, string? contextAsk = null);
}