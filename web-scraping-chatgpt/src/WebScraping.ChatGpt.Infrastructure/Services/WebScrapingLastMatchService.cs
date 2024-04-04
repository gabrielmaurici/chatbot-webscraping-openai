using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraping.ChatGpt.Domain.Models;
using WebScraping.ChatGpt.Domain.Services;

namespace WebScraping.ChatGpt.Infrastructure;

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
        var homeTeamName = GetHomeTeamName();
        var visitingTeamName = GetVisitingTeamName();
        var matchScore = GetMatchScore();
        var statisticsModel = GetStatistics();
        var urlMoments = GetUrlBestMoments();

        var response = new LastMatchModel
        {
            Tournament = tournamentName,
            Date = departureDateTime,
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
        Thread.Sleep(1000);
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

    private string GetMatchScore()
    {
        return _driver.FindElement(By.XPath("//div[@class='detailScore__wrapper']")).Text;
    }

    private StatisticsModel GetStatistics()
    {
        var statistics = new StatisticsModel();
        var statisticsElement = _driver.FindElements(By.XPath("//div[@class='_category_n1rcj_16']"));
        foreach (var statistic in statisticsElement)
        {
            var statisticName = statistic.Text.Replace("\n", " ");
            if (statisticName.Contains("Posse de bola"))
            {
                statistics.HomeBallPossession = statistic.FindElement(By.ClassName("_homeValue_bwnrp_10")).Text;
                statistics.VisitingBallPossession = statistic.FindElement(By.ClassName("_awayValue_bwnrp_14")).Text;
            }
            if (statisticName.Contains("Tentativas de gol"))
            {
                statistics.HomeGoalAttempts = statistic.FindElement(By.ClassName("_homeValue_bwnrp_10")).Text;
                statistics.VisitingGoalAttempts = statistic.FindElement(By.ClassName("_awayValue_bwnrp_14")).Text;
            }
            if (statisticName.Contains("Finalizações"))
            {
                statistics.HomeFinishes = statistic.FindElement(By.ClassName("_homeValue_bwnrp_10")).Text;
                statistics.VisitingFinishes = statistic.FindElement(By.ClassName("_awayValue_bwnrp_14")).Text;
            }
        }

        return statistics;
    }

    private string GetUrlBestMoments()
    {
        try 
        {
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