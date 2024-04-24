using WebScraping.OpenAI.Infrastructure.Services.LastMatchServices.AbstractService;

namespace WebScraping.OpenAI.Infrastructure.Services.LastMatchServices.ConcretServices;

public class FluminenseLastMatchService : WebScrapingLastMatchService
{
    private const string urlResultsFluminense = "https://www.flashscore.com.br/equipe/fluminense/EV9L3kU4/resultados/";
    public FluminenseLastMatchService() : base(urlResultsFluminense)
    {
    }
}