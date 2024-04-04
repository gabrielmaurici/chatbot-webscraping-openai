namespace WebScraping.ChatGpt.Infrastructure.Services;

public class FlamengoLastMatchService : WebScrapingLastMatchService
{
    private const string urlResultsFlamengo = "https://www.flashscore.com.br/equipe/flamengo/WjxY29qB/resultados/";
    public FlamengoLastMatchService() : base(urlResultsFlamengo)
    {
    }
}
