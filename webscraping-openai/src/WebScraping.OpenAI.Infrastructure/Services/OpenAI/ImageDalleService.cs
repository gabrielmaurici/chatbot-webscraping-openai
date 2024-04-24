using Newtonsoft.Json.Linq;
using WebScraping.OpenAI.Domain.Services.OpenAI;

namespace WebScraping.OpenAI.Infrastructure.Services.OpenAI;

public class ImageDalleService(IOpenAIApiService openAIApiService) : IImageDalleService
{
    private readonly IOpenAIApiService _openAIApiService = openAIApiService;
    private const string RESOURCE_IMAGES_GENERATIONS = "v1/images/generations";

    public async Task<string> GenerateImage(string imageDescription)
    {
        var content = CreateContentRequest(imageDescription);
        var request = await _openAIApiService.CreateRequest(
            resource: RESOURCE_IMAGES_GENERATIONS,
            method: HttpMethod.Post,
            content: content
        );
        var response = await _openAIApiService.SendRequest(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode)
            throw new Exception("Erro ao gerar imagem com a IA DAll-e" + responseContent);
      
        var imageUrl = DeserializeResponse(responseContent);
        return imageUrl;
    }

    private static object CreateContentRequest(string imageDescription)
    {
        return new
        {
            model = "dall-e-3",
            prompt = imageDescription,
            n = 1,
            size = "1024x1024",
            style = "vivid"
        };
    }

    private static string DeserializeResponse(string responseContent)
    {
        var jsonResponse = JObject.Parse(responseContent)!;
        var imageUrl = jsonResponse["data"]!
            .Select(data => data["url"]!)
            .First()!;
        return imageUrl.ToString();
    }
}
