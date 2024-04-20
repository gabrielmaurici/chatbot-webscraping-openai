using WebScraping.ChatGpt.Domain.Models.OpenAI;

namespace WebScraping.ChatGpt.Domain.Services.OpenAI;

public interface IImageDalleService
{
    Task<ImageDalleModel> GenerateImage(string imageDescription);
}
