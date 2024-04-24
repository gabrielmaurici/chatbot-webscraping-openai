using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebScraping.OpenAI.Domain.Services.OpenAI;

namespace WebScraping.OpenAI.Infrastructure.Services.OpenAI;

public class OpenAIApiService(HttpClient client, IConfiguration configuration) : IOpenAIApiService
{
    private readonly HttpClient _client = client;
    private readonly string _openAIApiKey = configuration.GetValue<string>("OPENAI_API_KEY") ??
        throw new ArgumentNullException("Não foi possível obter a chave de acesso Open AI API");

    public Task<HttpRequestMessage> CreateRequest(string resource, HttpMethod method, object? content = null)
    {
        HttpContent? httpContent = null;
        if (content != null) 
        {
            var serializedContent = JsonConvert.SerializeObject(content);
            httpContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
        }
        
        var request = new HttpRequestMessage(method, resource)
        {
            Content = httpContent
        };
        request.Headers.Add("Authorization", $"Bearer {_openAIApiKey}");

        return Task.FromResult(request);
    }

    public async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
    {
        return await _client.SendAsync(request);
    }
}