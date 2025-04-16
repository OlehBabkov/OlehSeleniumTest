using Microsoft.Extensions.Options;
using OlehSeleniumTest.Configuration;
using OlehSeleniumTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OlehSeleniumTest.Commands
{
    public class CustomCommands
    {
        private readonly CustomSettings _customSettings;

        public CustomCommands(IOptions<CustomSettings> options)
        {
            _customSettings = options.Value;
        }

        public void TakeScreenshot(IWebDriver driver, string stepName)
        {
            string screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile($"Screenshots/{stepName}.png");
        }

        public void WaitForTableRowData(
            IWebDriver driver, User expectedUser, int timeoutInSeconds = 5)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.Until(d =>
            {
                var rows = d.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"));

                foreach (var row in rows)
                {
                    var rowText = row.Text;

                    bool allMatch = rowText.Contains(expectedUser.FirstName, StringComparison.OrdinalIgnoreCase) &&
                                    rowText.Contains(expectedUser.LastName, StringComparison.OrdinalIgnoreCase) &&
                                    rowText.Contains(expectedUser.Email, StringComparison.OrdinalIgnoreCase) &&
                                    rowText.Contains(expectedUser.Age, StringComparison.OrdinalIgnoreCase) &&
                                    rowText.Contains(expectedUser.Salary, StringComparison.OrdinalIgnoreCase) &&
                                    rowText.Contains(expectedUser.Department, StringComparison.OrdinalIgnoreCase);

                    if (allMatch)
                    {
                        Console.WriteLine("User was found!");
                        Console.WriteLine(rowText);

                        return true;
                    }
                }

                System.Console.WriteLine("User wasn't found!");
                return false;
            });
        }

        public void WaitForTableRowDeletion(
            IWebDriver driver, string email, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.Until(d =>
            {
                var rows = d.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"));

                return !rows.Any(row => row.Text.Contains(email, StringComparison.OrdinalIgnoreCase));
            });
        }
    }
}