
using WebScraping.ChatGpt.Infrastructure;
using WebScraping.ChatGpt.Infrastructure.Services;

var teste = new WebScrapingNextMatchService();

var response = await teste.ExecuteScraping();
Console.WriteLine(response.ToString());

