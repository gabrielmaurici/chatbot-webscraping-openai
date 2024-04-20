namespace WebScraping.ChatGpt.Domain.Models.WebScraping;

public class LastMatchModel : MatchBaseModel
{
    public StatisticsModel Statistics { get; set;} = null!;
    public string UrlBestMoments {get; set;} = null!;

    public override string ToString()
    {
        return $"🏆 *Campeonato {Tournament}*\n\n" +
               $"📆 *Data:* {Date.Replace(".", "/").Replace(" ", " - ")} hrs\n" +
               $"🏟️ *Estádio*: {Stadium}\n" +
               $"🪧 *Placar:* {HomeTeam} {Score.Replace("\n", "")} {VisitingTeam}\n\n" +
               "📊 *Estatísticas*\n" +
               $"*Posse de bola* ⚽\n" +
               $"{HomeTeam} {Statistics.HomeBallPossession} - {Statistics.VisitingBallPossession} {VisitingTeam}\n\n" +
               $"*Tentativas de gol* 🥅\n" +
               $"{HomeTeam} {Statistics.HomeGoalAttempts} - {Statistics.VisitingGoalAttempts} {VisitingTeam}\n\n" +
               $"*Finalizações* ✅\n" +
               $"{HomeTeam} {Statistics.HomeFinishes} - {Statistics.VisitingFinishes} {VisitingTeam}\n\n" +
               $"🎥 Assita aos melhores momentos: {UrlBestMoments}";
    }
}