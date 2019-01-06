using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace wd
{
    [TestClass]
    public class SearchTest
    {
        private const string ApplicationToTestUrl = "http://avia.bilet.by/";
        private FirefoxDriver _firefox;

        [TestMethod]
        public void CheckForSourceAndDestCities()
        {
            _firefox = new FirefoxDriver();

            string depCityConst = "Минск";
            string arrCityConst = "Тбилиси";
            int depDate = 26;
            int arrDate = 27;

            // Go to the site
            _firefox.Navigate().GoToUrl(ApplicationToTestUrl);

            // Set the city of departure
            var inputFrom = _firefox.FindElement(By.Id("from_name"));
            inputFrom.Click();
            inputFrom.Clear();
            inputFrom.SendKeys(depCityConst);
            Thread.Sleep(2000);

            // Set the city of arrival
            var inputTo = _firefox.FindElement(By.Id("to_name"));
            inputTo.Click();
            inputTo.Clear();
            inputTo.SendKeys(arrCityConst);
            Thread.Sleep(2000);

            // Set departure date
            var dateFrom = _firefox.FindElement(By.Id("departure_date"));
            dateFrom.Click();
            Thread.Sleep(2000);
            var daysFrom = _firefox.FindElements(By.ClassName("ui-state-default"));
            daysFrom[depDate].Click();

            // Set arrival date
            var dateTo = _firefox.FindElement(By.Id("departure_date_1"));
            dateTo.Click();
            Thread.Sleep(2000);
            var daysTo = _firefox.FindElements(By.ClassName("ui-state-default"));
            daysTo[arrDate].Click();

            // Press searchButton
            IWebElement searchButton = _firefox.FindElement(By.XPath("//*[@id=\"page\"]/div[1]/div/div/div[1]/div[2]/form/div[2]/div/div[1]/div/input"));
            searchButton.Click();

            // Finding information by the system
            Thread.Sleep(10000);

            var depCityLi = _firefox.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[2]/li[3]"));
            var depCityLiSpan = _firefox.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[2]/li[3]/span[1]"));
            string depCity = depCityLi.Text.Replace(" " + depCityLiSpan.Text, "");

            var arrCityLi = _firefox.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[4]/li[3]"));
            var arrCityLiSpan = _firefox.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[4]/li[3]/span[1]"));
            string arrCity = arrCityLi.Text.Replace(" " + arrCityLiSpan.Text, "");

            Assert.AreEqual(depCityConst, depCity);
            Assert.AreEqual(arrCityConst, arrCity);

            Thread.Sleep(5000);
        }

        [TestCleanup]
        public void DriverClose()
        {
            _firefox.Quit();
        }
    }
}
