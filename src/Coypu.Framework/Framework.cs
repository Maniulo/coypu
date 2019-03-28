using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.IO;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Coypu
{
    public class Framework
    {
        public static string ResultsPath { get; private set; }

        static Framework()
        {
            ResultsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Results");
        }

        public static BrowserSession NewSession()
        {
            var sessionConfiguration = new SessionConfiguration();
            sessionConfiguration.Browser = Drivers.Browser.Firefox;
            return new BrowserSession(sessionConfiguration);
        }

        public static void Log(BrowserSession b, TestContext result)
        {
            if (result.Result.Outcome != ResultState.Success)
                Screenshot(b);
        }

        public static void Log(BrowserSession b, ScenarioContext result)
        {
            if (result.TestError != null)
                Screenshot(b);
        }

        public static void Screenshot(BrowserSession b)
        {
            if (!Directory.Exists(ResultsPath))
                Directory.CreateDirectory(ResultsPath);

            var screenshot = ((ITakesScreenshot)b.Native).GetScreenshot();
            screenshot.SaveAsFile(
                Path.Combine(ResultsPath, $"{DateTime.Now.ToString("yyyyMMdd Hmmss")} - {TestContext.CurrentContext.Test.Name}.jpg"),
                ScreenshotImageFormat.Jpeg);
        }
    }
}