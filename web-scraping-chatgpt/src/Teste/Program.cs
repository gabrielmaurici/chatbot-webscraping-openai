
using WebScraping.ChatGpt.Domain.Enums;
using WebScraping.ChatGpt.Infrastructure;
using WebScraping.ChatGpt.Infrastructure.Services;

var team = ETeams.Fluminense;


WebScrapingLastMatchService webScrapingService = team switch {
    ETeams.Fluminense => new FluminenseLastMatchService(),
    ETeams.Flamengo => new FlamengoLastMatchService(),
    ETeams.Brusque => new BrusqueLastMatchService(),
    _ => throw new Exception("Nenhum time correspondente")
};


var response = await webScrapingService.ExecuteScraping();
Console.WriteLine(response.ToString());

