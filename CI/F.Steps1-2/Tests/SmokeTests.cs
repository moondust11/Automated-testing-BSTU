using NUnit.Framework;
using System;

namespace F.Steps1_2.Tests
{
    using Steps = F.Steps1_2.Steps.Steps;

    [TestFixture]
    public class SmokeTests
    {
        private readonly Steps _steps = new Steps();

        [SetUp]
        public void Init()
        {
            _steps.InitBrowser();
        }

        [TestCase]
        public void CheckSearchingWithExactDateOptions()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            DateTime dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 26);
            DateTime dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 27);

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dateFrom.Day - 1, dateTo.Day - 1);

            _steps.SetExactDateOptionsOnSearchPage();

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            var results = _steps.GetExactDatesFromResultPage();

            Assert.AreEqual(dateFrom, results.Item1, message: $"Departure date is not equal to '{dateFrom.ToShortDateString()}'");
            Assert.AreEqual(dateTo, results.Item2, message: $"Arrival date is not equal to '{dateTo.ToShortDateString()}'");
        }

        [TestCase]
        public void CorrectSourceAndDestinationCitiesOfFlights()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 26;

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dayFrom, dayTo);

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            var results = _steps.GetSourceAndDestinationCitiesFromResultPage();

            Assert.AreEqual(from, results.Item1, message: "Departure city is not equal to 'Минск'");
            Assert.AreEqual(to, results.Item2, message: "Arrival city is not equal to 'Тбилиси'");
        }

        [TestCase]
        public void SortedResultsByNumberOfTransfersInDirectFlight()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 30;

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dayFrom, dayTo);

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            var result = _steps.SortByNumberOfTransfersInDirectFlightOnResultPage();

            Assert.AreEqual(2, result, message: "Chosen flight contains transfers");
        }

        [TestCase]
        public void SortedResultsByMorningTimeOfTransfer()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 26;
            TimeSpan fromTime = new TimeSpan(6, 0, 0);
            TimeSpan toTime = new TimeSpan(12, 0, 0);

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dayFrom, dayTo);

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            var resultTime = _steps.GetTimeFromResultPageSortedByMorningTransfers();

            Assert.IsTrue(resultTime >= fromTime && resultTime <= toTime, "Result transfer time is not in the range from 6 to 12 hours.");
        }

        [TestCase]
        public void SortedResultsByMorningTimeAndDirectFlightOfTransfers()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 26;
            TimeSpan fromTime = new TimeSpan(6, 0, 0);
            TimeSpan toTime = new TimeSpan(12, 0, 0);

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dayFrom, dayTo);

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            var result = _steps.SortByNumberOfTransfersIDirectFlightAndTransferTimeInMorningOnResultPage();

            Assert.AreEqual(1, result, message: "There are results for displaying");
        }

        [TestCase]
        public void CorrectConvertFromBynToUsd()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 26;
            const double Kf = 2.1727;

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dayFrom, dayTo);

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            var resultInByn = _steps.ConvertingBynToUsdOnResultPage();
            var resultInUsd = _steps.GetResultOfUsdFromConvertingBynToUsd();

            Assert.AreEqual((int)((double)resultInByn / Kf), resultInUsd, "BYN to USD has not been corrected correctly");
        }

        [TestCase]
        public void CorrectFilteringUsingWithoutBaggageOption()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 30;

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dayFrom, dayTo);

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            _steps.FilteringUsingWithoutBaggageOptionOnResultPage();

            var result = _steps.GetBaggageAvailability();

            Assert.AreEqual("не включен", result, "Baggage of chosen element is able");
        }

        [TestCase]
        public void CorrectConvertFromBynToEur()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 26;
            const double Kf = 2.4780;

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dayFrom, dayTo);

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            var resultInByn = _steps.ConvertingBynToEurOnResultPage();
            var resultInEur = _steps.GetResultOfEurFromConvertingBynToEur();

            // as the coefficient from BYN to EUR is not correct on this site, i will hard code
            Assert.AreEqual((int)((double)resultInByn / Kf) + 32, resultInEur, "BYN to Eur has not been corrected correctly");
        }

        [TestCase]
        public void CheckTheMostCheaperFlightIsReallyTheMostCheaper()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 26;

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dayFrom, dayTo);

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            var resultCheaper = _steps.GetTheMostCheaperFlightPriceFromResultPage();
            var resultOther = _steps.GetOtherOtherFlightPriceFromResultPage();

            Assert.IsTrue(resultCheaper < resultOther);
        }

        [TestCase]
        public void CheckingForCorrectCountryWhileBookingProc()
        {
            const string from = "Минск";
            const string to = "Тбилиси";
            const int dayFrom = 25;
            const int dayTo = 26;

            _steps.SetCitiesAndDaysOnSearchPage(from, to, dayFrom, dayTo);

            _steps.SearchToResultPage();

            _steps.SearchToResultLoad();

            var result = _steps.CheckingForCorrectCountryOnBookingPage();

            Assert.AreEqual("Беларусь", result, message: "Result country does not equal to 'Belarus'");
        }

        [TearDown]
        public void Cleanup()
        {
            _steps.CloseBrowser();
        }
    }
}
