
namespace WebScraping.OpenAI.Domain.Services.OpenAI;

public interface IImageDalleService
{
    Task<string> GenerateImage(string imageDescription);
}
