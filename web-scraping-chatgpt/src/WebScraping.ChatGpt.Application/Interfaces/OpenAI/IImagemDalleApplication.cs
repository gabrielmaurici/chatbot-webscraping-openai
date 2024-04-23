namespace WebScraping.ChatGpt.Application.Interfaces.OpenAI;

public interface IImagemDalleApplication
{
    Task<string> GenerateImage(string imageDescription);
}