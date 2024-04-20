using Newtonsoft.Json;
using WebScraping.ChatGpt.Domain.Models.OpenAI;
using WebScraping.ChatGpt.Domain.Services.OpenAI;

namespace WebScraping.ChatGpt.Infrastructure.Services.OpenAI;

public class ImageDalleService(IOpenAIApiService openAIApiService) : IImageDalleService
{
    private readonly IOpenAIApiService _openAIApiService = openAIApiService;
    private const string RESOURCE_IMAGES_GENERATIONS = "v1/images/generations";

    public async Task<ImageDalleModel> GenerateImage(string imageDescription)
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
        
        var imageModel = DeserializeResponse(responseContent);
        return imageModel;
    }

    private static object CreateContentRequest(string imageDescription)
    {
        return new
        {
            model = "dall-e-3",
            prompt = imageDescription,
            n = 1,
            size = "1024x1024",
            response_format = "b64_json",
            style = "natural"
        };
    }

    private static ImageDalleModel DeserializeResponse(string responseContent)
    {
        var imageModel = JsonConvert.DeserializeObject<ImageDalleModel>(responseContent)!;
        return imageModel;
    }
}
