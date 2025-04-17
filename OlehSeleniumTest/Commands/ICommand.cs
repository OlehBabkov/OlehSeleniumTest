using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public interface ICommand
    {
        string Name {get; }

        Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters);
    }
}