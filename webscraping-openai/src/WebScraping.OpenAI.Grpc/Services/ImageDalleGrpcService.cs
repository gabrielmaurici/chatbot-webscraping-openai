using Grpc.Core;
using WebScraping.OpenAI.Application.Interfaces.OpenAI;

namespace WebScraping.OpenAI.Grpc.Services;

public class ImageDalleGrpcService(IImagemDalleApplication imageDalleApplication) : ImageDalle.ImageDalleBase
{
    private readonly IImagemDalleApplication _imageDalleApplication = imageDalleApplication;

    public override async Task<GenerateImageReply> GenerateImage(GenerateImageRequest request, ServerCallContext context)
    {
        var response = await _imageDalleApplication.GenerateImage(request.ImageDescription);
        return new GenerateImageReply {
            Url = response
        };
    }
}