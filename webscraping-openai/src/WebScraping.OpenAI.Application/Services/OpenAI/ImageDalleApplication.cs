using WebScraping.OpenAI.Application.Interfaces.OpenAI;
using WebScraping.OpenAI.Domain.Services.OpenAI;

namespace WebScraping.OpenAI.Application.Services.OpenAI;

public class ImageDalleApplication(IImageDalleService imageDalleService) : IImageDalleApplication
{
    private readonly IImageDalleService _imageDalleService = imageDalleService;

    public async Task<string> GenerateImage(string imageDescription)
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