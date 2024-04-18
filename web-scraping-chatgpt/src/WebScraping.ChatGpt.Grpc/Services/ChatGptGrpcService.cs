using Grpc.Core;
using WebScraping.ChatGpt.Application.Interfaces;

namespace WebScraping.ChatGpt.Grpc.Services;

public class ChatGptGrpcService(IChatGptApplication chatGptApplication) : ChatGpt.ChatGptBase
{
    private readonly IChatGptApplication _chatGptApplication = chatGptApplication;

    public override async Task<AskQuestionReply> AskQuestion(AskQuestionRequest request, ServerCallContext context)
    {
        var response = await _chatGptApplication.AskQuestion(request.Ask);
        return new AskQuestionReply {
            ResponseIA = response
        };
    }

}