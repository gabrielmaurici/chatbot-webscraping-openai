namespace WebScraping.ChatGpt.Application.Interfaces.OpenAI;

public interface IChatGptApllication
{
    Task<string> AskQuestion(string ask);
}