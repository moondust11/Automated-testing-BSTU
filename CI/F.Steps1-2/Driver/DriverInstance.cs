using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace F.Steps1_2.Driver
{
    public class DriverInstance
    {
        private static IWebDriver _driver;

        private DriverInstance() { }

        public static IWebDriver GetInstance()
        {
            if (_driver != null) return _driver;

            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().ImplicitWait.Add(TimeSpan.FromSeconds(30));
            _driver.Manage().Window.Maximize();

            return _driver;
        }

        public static void CloseBrowser()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
