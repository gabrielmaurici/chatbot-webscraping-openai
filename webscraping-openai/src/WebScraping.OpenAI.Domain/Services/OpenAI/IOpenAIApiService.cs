namespace WebScraping.OpenAI.Domain.Services.OpenAI;

public interface IOpenAIApiService
{
    Task<HttpRequestMessage> CreateRequest(string resource, HttpMethod method, object? content = null);
    Task<HttpResponseMessage> SendRequest(HttpRequestMessage request);
}