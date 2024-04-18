using Microsoft.Extensions.Configuration;
using OpenAI.ChatGpt;
using OpenAI.ChatGpt.Models.ChatCompletion.Messaging;
using WebScraping.ChatGpt.Domain.Services;

namespace WebScraping.ChatGpt.Infrastructure.Services;

public class ChatGptService : IChatGptService
{
    private readonly OpenAiClient _client;

    public ChatGptService(IConfiguration configuration)
    {
        var openApiKey = configuration.GetValue<string>("OPENAI_API_KEY") ??
            throw new ArgumentNullException("Não foi possível obter a chave do ChatGpt");
        _client = new(openApiKey);
    }

    public async Task<string> AskQuestion(string ask, string? contextAsk = null)
    {
        var response = await _client.GetChatCompletions(new UserMessage(ask));
        return response;
    }
}
