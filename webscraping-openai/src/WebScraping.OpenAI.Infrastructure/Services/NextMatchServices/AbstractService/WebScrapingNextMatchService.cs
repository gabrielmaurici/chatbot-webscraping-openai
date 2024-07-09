using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraping.OpenAI.Domain.Models.WebScraping;
using WebScraping.OpenAI.Domain.Services.WebScraping;

namespace WebScraping.OpenAI.Infrastructure.Services.NextMatchServices.AbstractService;

public abstract class WebScrapingNextMatchService : IWebScrapingService<NextMatchModel>
{
    private readonly ChromeDriver _driver;
    private readonly string _urlTeamNextMatch;

    public WebScrapingNextMatchService(string urlTeamNextMatch)
    {
        ChromeOptions options = new();
        options.AddArgument("--headless");
        options.AddArgument("--window-size=1400,600");
        options.AddArguments("--disable-dev-shm-usage");

        _driver = new ChromeDriver(options);
        _urlTeamNextMatch = urlTeamNextMatch;
    }

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
        Thread.Sleep(300);
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
        try 
        { 

            return _driver.FindElement(By.XPath("//span[@class='tournamentHeader__country']")).Text;
        }
        catch 
        {
            return "Não foi possível obter no nome do campeonato";
        }
    }

    private string GetDepartureDateTime()
    {
        try 
        {
            return _driver.FindElement(By.XPath("//div[@class='duelParticipant__startTime']")).Text;
        }
        catch 
        {
            return "Não foi possível obter a data da partida";
        }
    }

    private string GetHomeTeamName()
    {
        try 
        {
        return _driver.FindElement(By.ClassName("duelParticipant__home"))
                      .FindElement(By.ClassName("participant__participantNameWrapper"))
                      .FindElement(By.ClassName("participant__participantName")).Text;
        }
        catch 
        {
            return "Não foi possível obter o nome do time da casa";
        }
    }

    private string GetVisitingTeamName()
    {
        try 
        {
            return _driver.FindElement(By.ClassName("duelParticipant__away"))
                        .FindElement(By.ClassName("participant__participantNameWrapper"))
                        .FindElement(By.ClassName("participant__participantName")).Text;
        }
        catch 
        {
            return "Não foi possível encontrar o nome do time visitante";
        }
    }

    private string GetStadiumName()
    {
        try 
        { 
            return _driver.FindElement(By.XPath("//span[text()='Estádio']/ancestor::div/following-sibling::div")).Text;
        }
        catch
        { 
            return "Não foi possível obter o nome do estádio";
        }
    }

}
