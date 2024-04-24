using Newtonsoft.Json.Linq;
using WebScraping.OpenAI.Domain.Services.OpenAI;

namespace WebScraping.OpenAI.Infrastructure.Services.OpenAI;

public class ChatGptService(IOpenAIApiService openAIApiService) : IChatGptService
{
    private readonly IOpenAIApiService _openAIApiService = openAIApiService;
    private const string RESOURCE_CHAT_COMPLETIONS = "v1/chat/completions";

    public async Task<string> AskQuestion(string ask, string? contextAsk = null)
    {
        var content = CreateContentRequest(ask);
        var request = await _openAIApiService.CreateRequest(
            resource: RESOURCE_CHAT_COMPLETIONS,
            method: HttpMethod.Post,
            content: content
        );
        var response = await _openAIApiService.SendRequest(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao enviar pergunta ao ChatGPT. " + responseContent);

        var messageChatGpt = DeserializeResponse(responseContent);

        return messageChatGpt.ToString();
    }

    private static object CreateContentRequest(string ask)
    {
        return new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = ask
                }
            }
        };
    }

    private static string DeserializeResponse(string responseContent)
    {
        var jsonResponse = JObject.Parse(responseContent)!;
        var messageChatGpt = jsonResponse["choices"]!
            .Select(choice => choice["message"]!["content"])
            .First()!;
        return messageChatGpt.ToString();
    }
}