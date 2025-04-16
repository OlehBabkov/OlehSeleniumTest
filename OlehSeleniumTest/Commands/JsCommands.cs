using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public class JsCommands
    {
        public void HideAds(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript(@"
                const elements = document.querySelectorAll('[id*=""google_ads""] , .ad, .ads, iframe, [style*=""z-index""]');
                elements.forEach(el => el.style.display = 'none');");
        }
    }
}