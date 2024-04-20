using WebScraping.ChatGpt.Application.Interfaces.OpenAI;
using WebScraping.ChatGpt.Domain.Models.OpenAI;
using WebScraping.ChatGpt.Domain.Services.OpenAI;

namespace WebScraping.ChatGpt.Application.Services.OpenAI;

public class ImageDalleApplication(IImageDalleService imageDalleService) : IImagemDalleApplication
{
    private readonly IImageDalleService _imageDalleService = imageDalleService;

    public async Task<ImageDalleModel> GenerateImage(string imageDescription)
    {
        Validate(imageDescription);

        if (imageDescription.StartsWith("!IA-imagem"))
            imageDescription = ReturnImageDescriptionOnly(imageDescription);

        return await _imageDalleService.GenerateImage(imageDescription);
    }

    private static void Validate(string imageDescription)
    {
        if (string.IsNullOrWhiteSpace(imageDescription))
            throw new InvalidOperationException("ImageDescription é um campo obrigatório");
    }

    private static string ReturnImageDescriptionOnly(string imageDescription)
    {
        imageDescription = imageDescription[11..];
        return imageDescription;
    }
}