using OlehSeleniumTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace OlehSeleniumTest.Commands
{
    public static class CustomCommands
    {
        public static void WaitForTableRowData(
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

        public static void WaitForTableRowDeletion(
            IWebDriver driver, string name, int timeoutInSeconds)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(d => d.FindElements(By.XPath($"//div[@class='rt-td' and text()='{name}']")).Count == 0);
        }
    }
}