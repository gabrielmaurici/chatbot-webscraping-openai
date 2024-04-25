namespace WebScraping.OpenAI.Domain.Models.WebScraping;

public class StatisticsModel
{
    public string HomeBallPossession { get; set; } = null!;
    public string VisitingBallPossession { get; set; } = null!;
    public string HomeGoalAttempts { get; set; } = null!;
    public string VisitingGoalAttempts { get; set; } = null!;
    public string HomeShotsOnGoal { get; set; } = null!;
    public string VisitingShotsOnGoal { get; set; } = null!;
}