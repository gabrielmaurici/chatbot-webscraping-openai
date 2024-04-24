namespace WebScraping.OpenAI.Application.Interfaces.OpenAI;

public interface IImagemDalleApplication
{
    Task<string> GenerateImage(string imageDescription);
}