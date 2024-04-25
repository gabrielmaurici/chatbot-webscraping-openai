using System.Security.Cryptography;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraping.OpenAI.Domain.Models.WebScraping;
using WebScraping.OpenAI.Domain.Services.WebScraping;

namespace WebScraping.OpenAI.Infrastructure.Services.LastMatchServices.AbstractService;

public abstract class WebScrapingLastMatchService(string teamResultsUrl) : IWebScrapingService<LastMatchModel>
{
    private readonly ChromeDriver _driver = new();
    private readonly string _teamResultsUrl = teamResultsUrl;

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
        var statisticsModel = GetStatistics();
        var urlMoments = GetUrlBestMoments();

        var response = new LastMatchModel
        {
            Tournament = tournamentName,
            Date = departureDateTime,
            Stadium = stadiumName,
            HomeTeam = homeTeamName,
            Score = matchScore,
            VisitingTeam = visitingTeamName,
            Statistics = statisticsModel,
            UrlBestMoments = urlMoments
        };

        _driver.Quit();
        return Task.FromResult(response);
    }

    private void GoToUrlOfLatestMatch()
    {
        _driver.Navigate().GoToUrl(_teamResultsUrl);
        Thread.Sleep(800);
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
        Thread.Sleep(800);
    }

    private string GetTournamentName()
    {
        return _driver.FindElement(By.XPath("//span[@class='tournamentHeader__country']")).Text;
    }

    private string GetDepartureDateTime()
    {
        return _driver.FindElement(By.XPath("//div[@class='duelParticipant__startTime']")).Text;
    }

    private string GetStadiumName()
    {
        return _driver.FindElement(By.ClassName("matchInfoItem__value")).Text;
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

    private StatisticsModel GetStatistics()
    {
        var buttonStatistics = _driver.FindElements(By.ClassName("_tabsSecondary_1b0gr_48"));
        foreach(var button in buttonStatistics) 
        {
            var buttonName = button.Text.Replace("\n", " ").ToUpper();
            if(buttonName.Contains("ESTATÍSTICAS"))
            {
                button.Click();
            }
        }
        Thread.Sleep(500);

        var statistics = new StatisticsModel();
        var statisticsElement = _driver.FindElements(By.XPath("//div[@class='_category_n1rcj_16']"));
        foreach (var statistic in statisticsElement)
        {
            var statisticName = statistic.Text.Replace("\n", " ").ToUpper();
            if (statisticName.Contains("POSSE DE BOLA"))
            {
                statistics.HomeBallPossession = statistic.FindElement(By.ClassName("_homeValue_bwnrp_10")).Text;
                statistics.VisitingBallPossession = statistic.FindElement(By.ClassName("_awayValue_bwnrp_14")).Text;
            }
            if (statisticName.Contains("TENTATIVAS DE GOL"))
            {
                statistics.HomeGoalAttempts = statistic.FindElement(By.ClassName("_homeValue_bwnrp_10")).Text;
                statistics.VisitingGoalAttempts = statistic.FindElement(By.ClassName("_awayValue_bwnrp_14")).Text;
            }
            if (statisticName.Contains("CHUTES NO GOL"))
            {
                statistics.HomeShotsOnGoal = statistic.FindElement(By.ClassName("_homeValue_bwnrp_10")).Text;
                statistics.VisitingShotsOnGoal = statistic.FindElement(By.ClassName("_awayValue_bwnrp_14")).Text;
            }
        }

        return statistics;
    }

    private string GetUrlBestMoments()
    {
        try 
        {
            var sumaryButton = _driver.FindElement(By.XPath("//*[@id='detail']/div[7]/div/a[1]/button"));
            sumaryButton.Click();
            Thread.Sleep(500);

            var bestMomentsElement = _driver.FindElement(By.ClassName("matchReportBoxes"));
            bestMomentsElement.Click();

            Thread.Sleep(1000);
            var urlMoments = _driver.FindElement(By.TagName("object")).GetAttribute("data");

            return urlMoments;
        }
        catch(Exception ex) 
        {
            if (ex is NoSuchElementException || ex is ElementNotInteractableException)
                return "Não foi possível encontrar o vídeo com os melhores momentos da partida";
            throw;
        }
    }
}