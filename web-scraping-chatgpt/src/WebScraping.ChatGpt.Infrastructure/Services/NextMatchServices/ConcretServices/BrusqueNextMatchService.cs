using WebScraping.ChatGpt.Infrastructure.Services.NextMatchServices.AbstractService;

namespace WebScraping.ChatGpt.Infrastructure.Services.LastMatchServices.ConcretServices;

public class BrusqueNextMatchService : WebScrapingNextMatchService
{
    private const string urlCalendarBrusque = "https://www.flashscore.com.br/equipe/brusque/MkwKrigb/calendario/";
    public BrusqueNextMatchService() : base(urlCalendarBrusque)
    {
    }
}
