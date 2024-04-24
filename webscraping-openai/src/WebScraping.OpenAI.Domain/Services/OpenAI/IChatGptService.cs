namespace WebScraping.OpenAI.Domain.Services.OpenAI;

public interface IChatGptService
{
    Task<string> AskQuestion(string ask, string? contextAsk = null);
}