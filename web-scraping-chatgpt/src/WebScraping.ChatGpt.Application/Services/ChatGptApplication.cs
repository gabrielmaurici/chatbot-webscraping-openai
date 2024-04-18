using WebScraping.ChatGpt.Application.Interfaces;
using WebScraping.ChatGpt.Domain.Services;

namespace WebScraping.ChatGpt.Application.Services;

public class ChatGptApplication(IChatGptService chatGptService) : IChatGptApplication
{
    private readonly IChatGptService _chatGptService = chatGptService;

    public async Task<string> AskQuestion(string ask)
    {
        Validate(ask);

        if (ask.StartsWith("!IA "))
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
        ask = ask[3..];
        return ask;
    }
}