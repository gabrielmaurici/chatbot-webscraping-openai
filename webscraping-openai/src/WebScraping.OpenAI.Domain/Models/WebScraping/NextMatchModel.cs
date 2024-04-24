namespace WebScraping.OpenAI.Domain.Models.WebScraping;

public class NextMatchModel : MatchBaseModel
{
    public override string ToString()
    {
        return $"🏆 *Campeonato {Tournament}*\n\n" +
               $"📆 *Data:* {Date.Replace(".", "/").Replace(" ", " - ")} hrs\n" +
               $"🏟️ *Estádio*: {Stadium}\n" +
               $"⚽ *Partida:* {HomeTeam} vs {VisitingTeam}";
    }
}
