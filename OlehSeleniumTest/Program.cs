using System.Text.Json;
using OlehSeleniumTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class Program
{
    static async Task Main(string[] args)
    {
        // Setup Chrome options.
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        // Start Chrome Browser.
        using var driver = new ChromeDriver(options);

        // Read test data from JSON.
        var jsonText = await File.ReadAllTextAsync("TestData/userData.json");
        var user = JsonSerializer.Deserialize<User>(jsonText);

        // Navigate to the Web Tables page
        driver.Navigate().GoToUrl("https://demoqa.com/webtables");

        // Take a screenshot
        TakeScreenshot(driver, "step_1_open_page");
    }

    private static void TakeScreenshot(IWebDriver driver, string stepName)
    {
        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        screenshot.SaveAsFile($"Screenshots/{stepName}.png");
    }
}