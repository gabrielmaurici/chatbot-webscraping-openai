namespace WebScraping.OpenAI.Application.Interfaces.OpenAI;

public interface IChatGptApllication
{
    Task<string> AskQuestion(string ask);
}