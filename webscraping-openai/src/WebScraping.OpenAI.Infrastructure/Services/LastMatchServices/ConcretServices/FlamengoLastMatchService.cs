using WebScraping.OpenAI.Infrastructure.Services.LastMatchServices.AbstractService;

namespace WebScraping.OpenAI.Infrastructure.Services.LastMatchServices.ConcretServices;

public class FlamengoLastMatchService : WebScrapingLastMatchService
{
    private const string urlResultsFlamengo = "https://www.flashscore.com.br/equipe/flamengo/WjxY29qB/resultados/";
    public FlamengoLastMatchService() : base(urlResultsFlamengo)
    {
    }
}
