namespace OlehSeleniumTest.Configuration
{
    public class CustomSettings
    {
        public string BaseUrl { get; set; } = string.Empty;

        public string ScreenshotsPath { get; set; } = string.Empty;

        public int TimeoutSeconds { get; set; }
    }
}