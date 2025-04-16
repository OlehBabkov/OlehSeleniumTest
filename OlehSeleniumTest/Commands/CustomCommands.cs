using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace OlehSeleniumTest.Commands
{
    public static class CustomCommands
    {
        public static void WaitForTableRowData(
            IWebDriver driver, string name, int timeoutInSeconds = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(d => d.FindElement(By.XPath($"//div[@class='rt-td' and text()='{name}']")));
        }

        public static void WaitForTableRowDeletion(
            IWebDriver driver, string name, int timeoutInSeconds)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(d => d.FindElements(By.XPath($"//div[@class='rt-td' and text()='{name}']")).Count == 0);
        }
    }
}