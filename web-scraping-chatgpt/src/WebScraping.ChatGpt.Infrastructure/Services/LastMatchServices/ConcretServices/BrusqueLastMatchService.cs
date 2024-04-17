using WebScraping.ChatGpt.Infrastructure.Services.LastMatchServices.AbstractService;

namespace WebScraping.ChatGpt.Infrastructure.Services.LastMatchServices.ConcretServices;

public class BrusqueLastMatchService : WebScrapingLastMatchService
{
    private const string urlResultsBrusque = "https://www.flashscore.com.br/equipe/brusque/MkwKrigb/resultados/";
    public BrusqueLastMatchService() : base(urlResultsBrusque)
    {
    }
}