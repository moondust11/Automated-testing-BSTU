using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using PageObject.Pages;

namespace PageObject
{
    [TestClass]
    public class SearchTest
    {
        private const string ApplicationToTestUrl = "http://avia.bilet.by/";
        private FirefoxDriver _firefox;

        [TestInitialize]
        public void Init()
        {
            _firefox = new FirefoxDriver();
        }

        [TestMethod]
        public void SortedResultsByMorningTimeOfTransfer()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 26;
            TimeSpan fromTime = new TimeSpan(6, 0, 0);
            TimeSpan toTime = new TimeSpan(12, 0, 0);

            var searchFromPage = new SearchFormPage(_firefox);

            searchFromPage.Navigate();

            var resultFromPage = searchFromPage.SearchToResultPage(from, to, dayFrom, dayTo);

            resultFromPage.SearchResultsLoad();

            var resultTime = resultFromPage.SortByMorningResults();

            Assert.IsTrue(resultTime >= fromTime && resultTime <= toTime, "Result transfer time is not in the range from 6 to 12 hours.");
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _firefox.Quit();
        }
    }
}
