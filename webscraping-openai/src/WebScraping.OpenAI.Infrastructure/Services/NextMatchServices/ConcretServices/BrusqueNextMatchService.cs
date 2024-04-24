using WebScraping.OpenAI.Infrastructure.Services.NextMatchServices.AbstractService;

namespace WebScraping.OpenAI.Infrastructure.Services.NextMatchServices.ConcretServices;

public class BrusqueNextMatchService : WebScrapingNextMatchService
{
    private const string urlCalendarBrusque = "https://www.flashscore.com.br/equipe/brusque/MkwKrigb/calendario/";
    public BrusqueNextMatchService() : base(urlCalendarBrusque)
    {
    }
}
