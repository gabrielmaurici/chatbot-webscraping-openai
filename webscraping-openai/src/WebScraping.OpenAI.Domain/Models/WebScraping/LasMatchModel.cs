namespace WebScraping.OpenAI.Domain.Models.WebScraping;

public class LastMatchModel : MatchBaseModel
{
    public string UrlBestMoments {get; set;} = null!;

    public override string ToString()
    {
        return $"🏆 *Campeonato {Tournament}*\n\n" +
               $"📆 *Data:* {Date.Replace(".", "/").Replace(" ", " - ")} hrs\n" +
               $"🏟️ *Estádio*: {Stadium.Replace("\n", " ")}\n" +
               $"🪧 *Placar:* {HomeTeam} {Score.Replace("\n", "")} {VisitingTeam}\n\n" +
               $"🎥 Assita aos melhores momentos: {UrlBestMoments}";
    }
}