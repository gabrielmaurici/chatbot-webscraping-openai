using WebScraping.ChatGpt.Infrastructure.Services.NextMatchServices.AbstractService;

namespace WebScraping.ChatGpt.Infrastructure.Services.NextMatchServices.ConcretServices;

public class FlamengoNextMatchService : WebScrapingNextMatchService
{
    private const string urlCalendarFlamengo = "https://www.flashscore.com.br/equipe/flamengo/WjxY29qB/calendario/";
    public FlamengoNextMatchService() : base(urlCalendarFlamengo)
    {
    }
}
