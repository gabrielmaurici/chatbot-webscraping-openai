namespace WebScraping.ChatGpt.Domain.Models;

public class LastMatchModel : MatchBaseModel
{
    public StatisticsModel Statistics { get; set;} = null!;
    public string UrlBestMoments {get; set;} = null!;

    public override string ToString()
    {
        return $"🏆 Campeonato {Tournament}\n\n" +
               $"📆 Data: {Date}\n" +
               $"⚽ {HomeTeam} {Score.Replace("\n", "")} {VisitingTeam}\n\n" +
               "📊 Estatísticas\n" +
               $"Posse de bola: {HomeTeam} - {Statistics.HomeBallPossession} | {Statistics.VisitingBallPossession} - {VisitingTeam}\n" +
               $"Tentativas de gol: {HomeTeam} - {Statistics.HomeGoalAttempts} | {Statistics.VisitingGoalAttempts} - {VisitingTeam}\n" +
               $"Finalizações: {HomeTeam} - {Statistics.HomeFinishes} | {Statistics.VisitingFinishes} - {VisitingTeam}\n\n" +
               $"Assita aos melhores momentos: {UrlBestMoments}";
    }
}