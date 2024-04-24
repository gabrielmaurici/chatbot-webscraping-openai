using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraping.OpenAI.Domain.Models.WebScraping;
using WebScraping.OpenAI.Domain.Services.WebScraping;

namespace WebScraping.OpenAI.Infrastructure.Services.NextMatchServices.AbstractService;

public abstract class WebScrapingNextMatchService(string urlTeamNextMatch) : IWebScrapingService<NextMatchModel>
{
    private readonly ChromeDriver _driver = new();
    private readonly string _urlTeamNextMatch = urlTeamNextMatch;

    public Task<NextMatchModel> ExecuteScraping()
    {
        GoToUrlOfNextMatch();
        ClickAcceptCookies();
        ClickOnNextMatch();
        _driver.SwitchTo().Window(_driver.WindowHandles.Last());

        var tournamentName = GetTournamentName();
        var departureDateTime = GetDepartureDateTime();
        var stadiumName = GetStadiumName();
        var homeTeamName = GetHomeTeamName();
        var visitingTeamName = GetVisitingTeamName();

        var nextMatch = new NextMatchModel {
            Tournament = tournamentName,
            Date = departureDateTime,
            Stadium = stadiumName,
            HomeTeam  = homeTeamName,
            VisitingTeam = visitingTeamName
        };

        _driver.Quit();

        return Task.FromResult(nextMatch);
    }

    private void GoToUrlOfNextMatch()
    {
        _driver.Navigate().GoToUrl(_urlTeamNextMatch);
        Thread.Sleep(1000);
    }

    private void ClickAcceptCookies()
    {
        var resultado = _driver.FindElement(By.XPath("//*[@id='onetrust-accept-btn-handler']"));
        resultado.Click();
    }

    private void ClickOnNextMatch()
    {
        var resultado = _driver.FindElement(By.ClassName("event__match"));
        resultado.Click();
        Thread.Sleep(1000);
    }

    private string GetTournamentName()
    {
        return _driver.FindElement(By.XPath("//span[@class='tournamentHeader__country']")).Text;
    }

    private string GetDepartureDateTime()
    {
        return _driver.FindElement(By.XPath("//div[@class='duelParticipant__startTime']")).Text;
    }

    private string GetHomeTeamName()
    {
        return _driver.FindElement(By.ClassName("duelParticipant__home"))
                      .FindElement(By.ClassName("participant__participantNameWrapper"))
                      .FindElement(By.ClassName("participant__participantName")).Text;
    }

    private string GetVisitingTeamName()
    {
        return _driver.FindElement(By.ClassName("duelParticipant__away"))
                      .FindElement(By.ClassName("participant__participantNameWrapper"))
                      .FindElement(By.ClassName("participant__participantName")).Text;
    }

    private string GetStadiumName()
    {
        return _driver.FindElement(By.ClassName("matchInfoItem__value")).Text;
    }

}
