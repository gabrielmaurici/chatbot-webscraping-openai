namespace WebScraping.OpenAI.Domain.Models.WebScraping;

public class MatchBaseModel
{
    public string Tournament { get; set; } = null!;
    public string Date { get; set; } = null!;
    public string HomeTeam { get; set; } = null!;
    public string Score { get; set; } = null!;
    public string VisitingTeam { get; set; } = null!;
    public string Stadium { get; set; } = null!;
}