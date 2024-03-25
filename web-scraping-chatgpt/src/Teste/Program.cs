
using WebScraping.ChatGpt.Infrastructure;

var teste = new WebScrapingLastMatchService();
var response = await teste.ExecuteScraping();
Console.WriteLine(response.ToString());

