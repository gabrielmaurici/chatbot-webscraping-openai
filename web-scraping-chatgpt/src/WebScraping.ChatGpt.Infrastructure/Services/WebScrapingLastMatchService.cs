using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraping.ChatGpt.Domain.Models;
using WebScraping.ChatGpt.Domain.Services;

namespace WebScraping.ChatGpt.Infrastructure;

public class WebScrapingLastMatchService : IWebScrapingService<LastMatchModel>
{
    private readonly ChromeDriver _driver;
    private const string URL = "https://www.flashscore.com.br/equipe/fluminense/EV9L3kU4/resultados/";

    public WebScrapingLastMatchService()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");

        _driver = new(options); 
    }

    public Task<LastMatchModel> ExecuteScraping()
    {
        GoToUrlOfLatestMatch();

        ClickOnResultOfLastMatch();
        _driver.SwitchTo().Window(_driver.WindowHandles.Last());

        var tournamentName = GetTournamentName();
        var departureDateTime = GetDepartureDateTime();
        var homeTeamName = GetHomeTeamName();
        var visitingTeamName = GetVisitingTeamName();
        var matchScore = GetMatchScore();
        
        var homeBallPossession = string.Empty;
        var visitingBallPossession = string.Empty;
        var homeGoalAttempts = string.Empty;
        var visitingGoalAttempts = string.Empty;
        var homeFinishes = string.Empty;
        var visitingFinishes = string.Empty;
        

        var teste = _driver.FindElements(By.XPath("//div[@class='_category_n1rcj_16']"));
        foreach(var item in teste) {
            var nome = item.Text.Replace("\n", " ");

            if (nome.Contains("Posse de bola")) {
                homeBallPossession = item.FindElement(By.ClassName("_homeValue_bwnrp_10")).Text;
                visitingBallPossession = item.FindElement(By.ClassName("_awayValue_bwnrp_14")).Text;
            }
            
            if (nome.Contains("Tentativas de gol")) {
                homeGoalAttempts = item.FindElement(By.ClassName("_homeValue_bwnrp_10")).Text;
                visitingGoalAttempts = item.FindElement(By.ClassName("_awayValue_bwnrp_14")).Text;
            }

            if (nome.Contains("Finalizações")) {
                homeFinishes = item.FindElement(By.ClassName("_homeValue_bwnrp_10")).Text;
                visitingFinishes = item.FindElement(By.ClassName("_awayValue_bwnrp_14")).Text;
            }

        }

        _driver.FindElement(By.XPath("//*[@id='detail']/div[7]/div/a[5]")).Click();
        Thread.Sleep(5000);
        var urlMoments = _driver.FindElement(By.XPath("//*[@id='detail']/div[8]/div[2]/div/object")).GetAttribute("data");

        var response = new LastMatchModel
        {
            Tournament = tournamentName,
            Date = departureDateTime,
            HomeTeam = homeTeamName,
            Score = matchScore,
            VisitingTeam = visitingTeamName,
            Statistics = new StatisticsModel {
                HomeBallPossession = homeBallPossession,
                VisitingBallPossession = visitingBallPossession,
                HomeGoalAttempts = homeGoalAttempts,
                VisitingGoalAttempts = visitingGoalAttempts,
                HomeFinishes = homeFinishes,
                VisitingFinishes = visitingFinishes 
            },
            UrlBestMoments = urlMoments
        };

        _driver.Quit();

        return Task.FromResult(response);
    }

    private void GoToUrlOfLatestMatch()
    {
        _driver.Navigate().GoToUrl(URL);
        Thread.Sleep(3000);
    }

    private void ClickOnResultOfLastMatch()
    {
        var resultado = _driver.FindElement(By.XPath("//div[@class='event__match event__match--static event__match--twoLine']"));
        resultado.Click();
        Thread.Sleep(5000);
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

    private string GetMatchScore()
    {
        return _driver.FindElement(By.XPath("//div[@class='detailScore__wrapper']")).Text;
    }

    private string GetHomeBallPossession()
    {
        return _driver.FindElement(By.XPath("//div[@class='_value_1c6mj_5 _homeValue_1c6mj_10']")).Text;
    }

    private string GetVisitingBallPossession()
    {
        return _driver.FindElement(By.XPath("//div[@class='_value_1c6mj_5 _awayValue_1c6mj_14']")).Text;
    }

    private string GetHomeGoalAttempts()
    {
        return _driver.FindElement(By.XPath("//div[@class='_value_1c6mj_5 _homeValue_1c6mj_10']")).Text;
    }

    private string GetVisitingGoalAttempts()
    {
        return _driver.FindElement(By.XPath("//div[@class='_value_1c6mj_5 _homeValue_1c6mj_10']")).Text;
    }
}