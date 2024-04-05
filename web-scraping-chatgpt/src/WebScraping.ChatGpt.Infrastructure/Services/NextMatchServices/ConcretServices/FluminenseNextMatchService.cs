using WebScraping.ChatGpt.Infrastructure.Services.NextMatchServices.AbstractService;

namespace WebScraping.ChatGpt.Infrastructure.Services.NextMatchServices.ConcretServices;

public class FluminenseNextMatchService : WebScrapingNextMatchService
{
    private const string urlCalendarFluminense = "https://www.flashscore.com.br/equipe/fluminense/EV9L3kU4/calendario/";
    public FluminenseNextMatchService() : base(urlCalendarFluminense)
    {
    }
}
