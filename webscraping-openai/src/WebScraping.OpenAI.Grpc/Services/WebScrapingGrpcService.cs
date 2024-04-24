using Grpc.Core;
using WebScraping.OpenAI.Application.Interfaces.WebScrapingSoccer;

namespace WebScraping.OpenAI.Grpc.Services;

public class WebScrapingGrpcService(
    ILastMatchApplication lastMatchApplication,
    INextMatchApplication nextMatchApplication) : WebScraping.WebScrapingBase
{
    private readonly ILastMatchApplication _lastMatchApplication = lastMatchApplication;
    private readonly INextMatchApplication _nextMatchApplication = nextMatchApplication;

    public override async Task<LastMatchReply> GetLastMatch(LastMatchRequest request, ServerCallContext context)
    {
        var response = await _lastMatchApplication.Get(request.Team);
        return new LastMatchReply {
            LastMatch = response
        };
    }

    public override async Task<NextMatchReply> GetNextMatch(NextMatchRequest request, ServerCallContext context)
    {
       var response = await _nextMatchApplication.Get(request.Team);
        return new NextMatchReply {
            NextMatch = response
        };
    }
}