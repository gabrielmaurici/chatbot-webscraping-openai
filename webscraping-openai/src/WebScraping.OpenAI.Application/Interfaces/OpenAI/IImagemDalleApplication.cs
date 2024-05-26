namespace WebScraping.OpenAI.Application.Interfaces.OpenAI;

public interface IImageDalleApplication
{
    Task<string> GenerateImage(string imageDescription);
}