using WebScraping.ChatGpt.Domain.Models.OpenAI;

namespace WebScraping.ChatGpt.Application.Interfaces.OpenAI;

public interface IImagemDalleApplication
{
    Task<ImageDalleModel> GenerateImage(string imageDescription);
}