using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Threading;

namespace PageObject.Pages
{
    class ResultFormPage: BasePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id=\"tabs\"]/ul/li[1]/a")]
        private IWebElement transfersButton;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"tabs\"]/div/div[1]/div/div[2]/ul/li[3]/div")]
        private IWebElement morningTransferRadio;

        public ResultFormPage(IWebDriver webDriver) : base(webDriver)
        { }

        public void SearchResultsLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.UrlContains("search/results"));
            Thread.Sleep(1000);
        }

        public TimeSpan SortByMorningResults()
        {
            Thread.Sleep(1000);

            transfersButton.Click();

            Thread.Sleep(1000);

            morningTransferRadio.Click();

            Thread.Sleep(8000);
        
            var firstFlightWithTransfersString = Driver.FindElement(By.XPath("/html/body/div[1]/main/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/strong[18]"));
            var firstFlightWithTransfers = firstFlightWithTransfersString.FindElement(By.XPath(".."));
            firstFlightWithTransfers.Click();

            var flightDetails = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section/div[1]/ul/li[1]/label/ul/li[2]/a"));
            flightDetails.Click();

            Thread.Sleep(2000);

            var resultStringTime = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/section/div[3]/ul/li[4]/span/div[2]/span[2]/ul[2]/li[1]/strong")).Text;

            return TimeSpan.ParseExact(resultStringTime, "hh\\:mm", CultureInfo.InvariantCulture);
        }
    }
}
