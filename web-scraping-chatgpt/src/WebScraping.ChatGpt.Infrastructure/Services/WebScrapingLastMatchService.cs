
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraping.ChatGpt.Domain.Models;
using WebScraping.ChatGpt.Domain.Services;

namespace WebScraping.ChatGpt.Infrastructure;

public class WebScrapingLastMatchService : IWebScrapingService<LastMatchModel>
{
    private readonly ChromeDriver _driver;

    public WebScrapingLastMatchService()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");

        _driver = new(options) 
        {
            Url = "https://www.flashscore.com.br/equipe/fluminense/EV9L3kU4/resultados/"
        };
    }

    public Task<LastMatchModel> ExecuteScraping()
    {
        Thread.Sleep(3000);

        var resultado = _driver.FindElement(By.XPath("//div[@class='event__match event__match--static event__match--twoLine']"));
        resultado.Click();
        Thread.Sleep(5000);

        _driver.SwitchTo().Window(_driver.WindowHandles.Last());

        var campeonato = _driver.FindElement(By.XPath("//span[@class='tournamentHeader__country']")).Text;
        var horarioPartida = _driver.FindElement(By.XPath("//div[@class='duelParticipant__startTime']")).Text;

        var equipeMandante = 
            _driver.FindElement(By.ClassName("duelParticipant__home"))
                .FindElement(By.ClassName("participant__participantNameWrapper"))
                .FindElement(By.ClassName("participant__participantName")).Text;

        var equipeVisitante = 
            _driver.FindElement(By.ClassName("duelParticipant__away"))
                .FindElement(By.ClassName("participant__participantNameWrapper"))
                .FindElement(By.ClassName("participant__participantName")).Text;

        var scorePartida = _driver.FindElement(By.XPath("//div[@class='detailScore__wrapper']")).Text;

        var response = new LastMatchModel {
            Tournament = campeonato,
            Date = horarioPartida,
            HomeTeam = equipeMandante,
            Score = scorePartida,
            VisitingTeam = equipeVisitante,
            Statistics = new StatisticsModel(),
            UrlBestMoments = "asdfkasdklf"
        };

        _driver.Quit();

        return Task.FromResult(response);
    }
}
