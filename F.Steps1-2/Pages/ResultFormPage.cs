using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace F.Steps1_2.Pages
{
    public class ResultFormPage: BasePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id=\"tabs\"]/ul/li[1]/a")]
        private IWebElement transfersButton;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"tabs\"]/div/div[1]/div/div[2]/ul/li[3]/div")]
        private IWebElement morningTransfersRadio;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"tabs\"]/div/div[1]/div/div[1]/ul/li[2]/div")]
        private IWebElement directFlightCheckbox;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"content\"]/div/div[1]/div[1]/ul/li[3]")]
        private IWebElement convertToUsd;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"content\"]/div/div[1]/div[1]/ul/li[4]/label")]
        private IWebElement convertToEur;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"content\"]/div/div[1]/div[2]/ul/li[2]")]
        private IWebElement withoutBaggage;
        
        public ResultFormPage(IWebDriver driver) : base(driver)
        { }

        public void SearchResultsLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.UrlContains("search/results"));
            Thread.Sleep(1000);
        }

        public TimeSpan SortByMorningTransfers()
        {
            Thread.Sleep(1000);

            transfersButton.Click();

            Thread.Sleep(1000);

            morningTransfersRadio.Click();

            Thread.Sleep(7000);

            var firstFlightWithTransfersString = Driver.FindElement(By.XPath("/html/body/div[1]/main/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/strong[18]"));
            var firstFlightWithTransfers = firstFlightWithTransfersString.FindElement(By.XPath(".."));
            firstFlightWithTransfers.Click();

            Thread.Sleep(3000);

            var flightDetails = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section/div[1]/ul/li[1]/label/ul/li[2]/a"));
            flightDetails.Click();

            Thread.Sleep(8000);

            var resultStringTime = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/section/div[3]/ul/li[4]/span/div[2]/span[2]/ul[2]/li[1]/strong")).Text;

            return TimeSpan.ParseExact(resultStringTime, "hh\\:mm", CultureInfo.InvariantCulture);
        }

        public int SortByNumberOfTransfersInDirectFlight()
        {
            Thread.Sleep(1000);

            transfersButton.Click();

            Thread.Sleep(1000);

            directFlightCheckbox.Click();

            Thread.Sleep(5000);

            var firstDirectFlightString = Driver.FindElement(By.XPath("/html/body/div[1]/main/div/div[2]/div[1]/div[1]/div[2]/div[2]/div/strong[18]"));
            var firstDirectFlight = firstDirectFlightString.FindElement(By.XPath(".."));
            firstDirectFlight.Click();

            Thread.Sleep(3000);

            var ulElement = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section/div[1]/ul/li/label/span/span/ul[3]"));

            return ulElement.FindElements(By.CssSelector("li")).Count;
        }

        public int SortByNumberOfTransfersInDirectFlightAndTransferTimeInMorning()
        {
            Thread.Sleep(1000);

            transfersButton.Click();

            Thread.Sleep(1000);

            directFlightCheckbox.Click();

            Thread.Sleep(3000);

            morningTransfersRadio.Click();

            Thread.Sleep(3000);

            return Driver.FindElements(By.XPath("//*[@id=\"content\"]/div/div[2]/div[1]/div/h2")).Count;
        }

        public Tuple<string, string> GetSourceAndDestinationCities()
        {
            var depCityLi = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[2]/li[3]"));
            var depCityLiSpan = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[2]/li[3]/span[1]"));
            string depCity = depCityLi.Text.Replace(" " + depCityLiSpan.Text, "");

            var arrCityLi = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[4]/li[3]"));
            var arrCityLiSpan = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[4]/li[3]/span[1]"));
            string arrCity = arrCityLi.Text.Replace(" " + arrCityLiSpan.Text, "");

            return new Tuple<string, string>(depCity, arrCity);
        }

        public Tuple<DateTime, DateTime> GetExactDates()
        {
            var depDateString = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li[1]/label/span/span/ul[2]/li[2]")).Text;
            Match depDayMatch = Regex.Match(depDateString, @"^\d*");
            Match depMonthMatch = Regex.Match(depDateString, @"\w{4}\w+");
            Match depYearMatch = Regex.Match(depDateString, @"\d{4}");
            var depDate = DateTime.ParseExact(depDayMatch.Captures[0].Value + ":" + Utils.MonthHelper.DetermineMonthByName(depMonthMatch.Captures[0].Value) + ":" + depYearMatch.Captures[0].Value, "dd\\:MMMM\\:yyyy", CultureInfo.GetCultureInfo("en-US"));


            var arrDateString = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li[1]/label/span/span/ul[4]/li[2]")).Text;
            Match arrDayMatch = Regex.Match(arrDateString, @"^\d*");
            Match arrMonthMatch = Regex.Match(arrDateString, @"\w{4}\w+");
            Match arrYearMatch = Regex.Match(arrDateString, @"\d{4}");
            var arrDate = DateTime.ParseExact(arrDayMatch.Captures[0].Value + ":" + Utils.MonthHelper.DetermineMonthByName(arrMonthMatch.Captures[0].Value) + ":" + arrYearMatch.Captures[0].Value, "dd\\:MMMM\\:yyyy", CultureInfo.GetCultureInfo("en-US"));

            return new Tuple<DateTime, DateTime>(depDate, arrDate);
        }

        public int ConvertFromBynToUsd()
        {
            Thread.Sleep(1000);

            var firstFlightString = Driver.FindElement(By.XPath("/html/body/div[1]/main/div/div[2]/div[1]/div[1]/div[2]/div[2]/div/strong[18]")).Text;
            Match firstFlightPriceMatch = Regex.Match(firstFlightString, @"^\d*");

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"content\"]/div/div[1]/div[1]/ul/li[3]")));
            convertToUsd.Click();

            Thread.Sleep(3000);

            return int.Parse(firstFlightPriceMatch.Captures[0].Value);
        }

        public int GetResultOfUsdAfterConvertingFromBynToUsd()
        {
            var firstFlightString = Driver.FindElement(By.XPath("/html/body/div[1]/main/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/strong[2]")).Text;
            Match firstFlightPriceMatch = Regex.Match(firstFlightString, @"^\d*");

            return int.Parse(firstFlightPriceMatch.Captures[0].Value);
        }

        public void FilteringUsingWithoutBaggageOption()
        {
            Thread.Sleep(1000);

            withoutBaggage.Click();

            Thread.Sleep(3000);
        }

        public string GetBaggageAvailabilityResult()
        {
            return Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section/div[1]/ul/li/label/ul/li[4]/span[2]")).Text;
        }

        public int ConvertFromBynToEur()
        {
            Thread.Sleep(1000);

            var firstFlightString = Driver.FindElement(By.XPath("/html/body/div[1]/main/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/strong[18]")).Text;
            Match firstFlightPriceMatch = Regex.Match(firstFlightString, @"^\d*");

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"content\"]/div/div[1]/div[1]/ul/li[4]/label")));
            convertToUsd.Click();

            Thread.Sleep(3000);

            return int.Parse(firstFlightPriceMatch.Captures[0].Value);
        }

        public int GetResultOfUsdAfterConvertingFromBynToEur()
        {
            var firstFlightString = Driver.FindElement(By.XPath("/html/body/div[1]/main/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/strong[2]")).Text;
            Match firstFlightPriceMatch = Regex.Match(firstFlightString, @"^\d*");

            return int.Parse(firstFlightPriceMatch.Captures[0].Value);
        }

        public int GetTheMostCheaperFlightPrice()
        {
            var theMostCheaperFlightPriceText = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[3]/div[3]/span/strong[18]")).Text;
            Match theMostCheaperFlightPriceMatch = Regex.Match(theMostCheaperFlightPriceText, @"^\d*");

            return int.Parse(theMostCheaperFlightPriceMatch.Captures[0].Value);
        }

        public int GetOtherFlightPrice()
        {
            var otherFlightPriceText = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[2]/div[3]/div[3]/span/strong[18]")).Text;
            Match otherFlightPriceMatch = Regex.Match(otherFlightPriceText, @"^\d*");

            return int.Parse(otherFlightPriceMatch.Captures[0].Value);
        }

        public BookingPage CheckingForCorrectCountryOnBookingOnBookingPage()
        {
            var chooseInput = Driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[3]/div[2]/input"));
            chooseInput.Click();

            return new BookingPage(Driver);
        }
    }
}
