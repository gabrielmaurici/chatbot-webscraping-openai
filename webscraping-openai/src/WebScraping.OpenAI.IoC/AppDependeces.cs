
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebScraping.OpenAI.Application.Interfaces.OpenAI;
using WebScraping.OpenAI.Application.Interfaces.WebScrapingSoccer;
using WebScraping.OpenAI.Application.Services.OpenAI;
using WebScraping.OpenAI.Application.Services.WebScrapingSoccer;
using WebScraping.OpenAI.Domain.Services.OpenAI;
using WebScraping.OpenAI.Infrastructure.Services.OpenAI;

namespace WebScraping.OpenAI.IoC;

public static class AppDependeces
{
    public static IServiceCollection AddDomainDependeces(this IServiceCollection services, IConfiguration configuration) {

        services.AddHttpClient<IOpenAIApiService, OpenAIApiService>(client => 
            client.BaseAddress = new Uri(configuration.GetValue<string>("open-ai-api")!)
        );
        services.AddScoped<IChatGptService, ChatGptService>();
        services.AddScoped<IImageDalleService, ImageDalleService>();
        return services;
    }

    public static IServiceCollection AddApllicationDependeces(this IServiceCollection services)
        => services.AddScoped<ILastMatchApplication, LastMatchApplication>()
                   .AddScoped<INextMatchApplication, NextMatchApplication>()
                   .AddScoped<IChatGptApllication, ChatGptApplication>()
                   .AddScoped<IImageDalleApplication, ImageDalleApplication>();
}
