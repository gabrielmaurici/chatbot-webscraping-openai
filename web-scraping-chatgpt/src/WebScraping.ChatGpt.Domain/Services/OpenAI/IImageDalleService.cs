
namespace WebScraping.ChatGpt.Domain.Services.OpenAI;

public interface IImageDalleService
{
    Task<string> GenerateImage(string imageDescription);
}
