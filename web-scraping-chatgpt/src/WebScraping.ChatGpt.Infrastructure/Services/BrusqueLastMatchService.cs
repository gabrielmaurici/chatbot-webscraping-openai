namespace WebScraping.ChatGpt.Infrastructure.Services;

public class BrusqueLastMatchService : WebScrapingLastMatchService
{
    private const string urlResultsBrusque = "https://www.flashscore.com.br/equipe/brusque/MkwKrigb/resultados/";
    public BrusqueLastMatchService() : base(urlResultsBrusque)
    {
    }
}
