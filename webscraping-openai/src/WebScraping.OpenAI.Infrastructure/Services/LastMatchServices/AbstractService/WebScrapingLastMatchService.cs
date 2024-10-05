using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraping.OpenAI.Domain.Models.WebScraping;
using WebScraping.OpenAI.Domain.Services.WebScraping;

namespace WebScraping.OpenAI.Infrastructure.Services.LastMatchServices.AbstractService;

public abstract class WebScrapingLastMatchService : IWebScrapingService<LastMatchModel>
{
    private readonly ChromeDriver _driver;
    private readonly string _teamResultsUrl;

    public WebScrapingLastMatchService(string teamResultsUrl)
    {
        ChromeOptions options = new();
        options.AddArgument("--headless");
        options.AddArgument("--window-size=1400,600");
        options.AddArguments("--disable-dev-shm-usage");

        _driver = new ChromeDriver(options);
        _teamResultsUrl = teamResultsUrl;
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }


    public Task<LastMatchModel> ExecuteScraping()
    {
        GoToUrlOfLatestMatch();
        ClickAcceptCookies();
        ClickOnResultOfLastMatch();
        _driver.SwitchTo().Window(_driver.WindowHandles.Last());

        var tournamentName = GetTournamentName();
        var departureDateTime = GetDepartureDateTime();
        var stadiumName = GetStadiumName();
        var homeTeamName = GetHomeTeamName();
        var visitingTeamName = GetVisitingTeamName();
        var matchScore = GetMatchScore();
        var urlMoments = GetUrlBestMoments();

        var response = new LastMatchModel
        {
            Tournament = tournamentName,
            Date = departureDateTime,
            Stadium = stadiumName,
            HomeTeam = homeTeamName,
            Score = matchScore,
            VisitingTeam = visitingTeamName,
            UrlBestMoments = urlMoments
        };

        _driver.Quit();
        return Task.FromResult(response);
    }

    private void GoToUrlOfLatestMatch()
    {
        _driver.Navigate().GoToUrl(_teamResultsUrl);
    }

    private void ClickAcceptCookies()
    {
        var resultado = _driver.FindElement(By.XPath("//*[@id='onetrust-accept-btn-handler']"));
        resultado.Click();
    }

    private void ClickOnResultOfLastMatch()
    {
        var resultado = _driver.FindElement(By.ClassName("event__match"));
        resultado.Click();
    }

    private string GetTournamentName()
    {
        try 
        {
            return _driver.FindElement(By.XPath("//span[@class='tournamentHeader__country']")).Text;
        }
        catch 
        {
            return "Não foi possível obter o nome do campeonato";
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

    private string GetStadiumName()
    {
        try {
            return _driver.FindElement(By.XPath("//span[text()='Estádio']/ancestor::div/following-sibling::div")).Text;
        }
        catch 
        {
            return "Não foi possível obter o nome do estádio";
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
            return "não foi possível obter o nome do time visitante";
        }
    }

    private string GetMatchScore()
    {
        try 
        {
            return _driver.FindElement(By.XPath("//div[@class='detailScore__wrapper']")).Text;
        }
        catch 
        {
            return "Não foi possível obter o placar da partida";
        }
    }

    private string GetUrlBestMoments()
    {
        try 
        {
            var sumaryButton = _driver.FindElement(By.XPath("//*[@id='detail']/div[7]/div/a[1]/button"));
            sumaryButton.Click();

            var bestMomentsElement = _driver.FindElement(By.ClassName("matchReportBoxes"));
            bestMomentsElement.Click();

            var urlMoments = _driver.FindElement(By.TagName("object")).GetAttribute("data");

            return urlMoments;
        }
        catch
        {
            return "Não foi possível encontrar o vídeo com os melhores momentos da partida";
        }
    }
}