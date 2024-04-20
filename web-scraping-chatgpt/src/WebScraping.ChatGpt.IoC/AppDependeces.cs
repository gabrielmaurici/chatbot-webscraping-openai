
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebScraping.ChatGpt.Application.Interfaces.OpenAI;
using WebScraping.ChatGpt.Application.Interfaces.WebScrapingSoccer;
using WebScraping.ChatGpt.Application.Services.OpenAI;
using WebScraping.ChatGpt.Application.Services.WebScrapingSoccer;
using WebScraping.ChatGpt.Domain.Services.OpenAI;
using WebScraping.ChatGpt.Infrastructure.Services.OpenAI;

namespace WebScraping.ChatGpt.IoC;

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
                   .AddScoped<IImagemDalleApplication, ImageDalleApplication>();
}
