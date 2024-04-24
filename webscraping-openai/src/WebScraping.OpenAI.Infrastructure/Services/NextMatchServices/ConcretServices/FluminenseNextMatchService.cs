using WebScraping.OpenAI.Infrastructure.Services.NextMatchServices.AbstractService;

namespace WebScraping.OpenAI.Infrastructure.Services.NextMatchServices.ConcretServices;

public class FluminenseNextMatchService : WebScrapingNextMatchService
{
    private const string urlCalendarFluminense = "https://www.flashscore.com.br/equipe/fluminense/EV9L3kU4/calendario/";
    public FluminenseNextMatchService() : base(urlCalendarFluminense)
    {
    }
}
