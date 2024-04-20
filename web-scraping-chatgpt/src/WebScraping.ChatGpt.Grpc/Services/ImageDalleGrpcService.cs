using Grpc.Core;
using WebScraping.ChatGpt.Application.Interfaces.OpenAI;

namespace WebScraping.ChatGpt.Grpc.Services;

public class ImageDalleGrpcService(IImagemDalleApplication imageDalleApplication) : ImageDalle.ImageDalleBase
{
    private readonly IImagemDalleApplication _imageDalleApplication = imageDalleApplication;

    public override async Task<GenerateImageReply> GenerateImage(GenerateImageRequest request, ServerCallContext context)
    {
        var response = await _imageDalleApplication.GenerateImage(request.ImageDescription);
        return new GenerateImageReply {
            RevisedPrompt = response.Revised_prompt,
            Base64 = response.B64_json
        };
    }
}