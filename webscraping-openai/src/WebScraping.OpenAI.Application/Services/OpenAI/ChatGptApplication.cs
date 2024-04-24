using WebScraping.OpenAI.Application.Interfaces.OpenAI;
using WebScraping.OpenAI.Domain.Services.OpenAI;

namespace WebScraping.OpenAI.Application.Services.OpenAI;

public class ChatGptApplication(IChatGptService chatGptService) : IChatGptApllication
{
    private readonly IChatGptService _chatGptService = chatGptService;

    public async Task<string> AskQuestion(string ask)
    {
        Validate(ask);

        if (ask.StartsWith("!IA-chat"))
            ask = ReturnQuestionOnly(ask);

        return await _chatGptService.AskQuestion(ask, null);
    }

    private static void Validate(string ask)
    {
        if (string.IsNullOrWhiteSpace(ask))
            throw new InvalidOperationException("Ask é um campo obrigatório");
    }

    private static string ReturnQuestionOnly(string ask)
    {
        ask = ask[9..];
        return ask;
    }
}