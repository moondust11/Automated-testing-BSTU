using System;
using F.Steps1_2.Pages;

namespace F.Steps1_2.Steps
{
    public class Steps: BaseSteps
    {
        private ResultFormPage resultPage;
        private SearchFormPage searchFormPage;

        public void SearchToResultPage()
        {
            resultPage = searchFormPage.SearchToResultPage();
        }

        public void SetCitiesAndDaysOnSearchPage(string from, string to, int dayFrom, int dayTo)
        {
            var page = new SearchFormPage(Driver);
            searchFormPage = page;

            searchFormPage.Navigate();

            searchFormPage.SetCitiesAndDays(from, to, dayFrom, dayTo);
        }

        public void SearchToResultLoad()
        {
            resultPage.SearchResultsLoad();
        }

        public TimeSpan GetTimeFromResultPageSortedByMorningTransfers()
        {
            return resultPage.SortByMorningTransfers();
        }

        public Tuple<string, string> GetSourceAndDestinationCitiesFromResultPage()
        {
            return resultPage.GetSourceAndDestinationCities();
        }

        public void SetExactDateOptionsOnSearchPage()
        {
            searchFormPage.SetExactDateOptions();
        }

        public Tuple<DateTime, DateTime> GetExactDatesFromResultPage()
        {
            return resultPage.GetExactDates();
        }

        public int SortByNumberOfTransfersInDirectFlightOnResultPage()
        {
            return resultPage.SortByNumberOfTransfersInDirectFlight();
        }

        public int SortByNumberOfTransfersIDirectFlightAndTransferTimeInMorningOnResultPage()
        {
            return resultPage.SortByNumberOfTransfersInDirectFlightAndTransferTimeInMorning();
        }

        public int ConvertingBynToUsdOnResultPage()
        {
            return resultPage.ConvertFromBynToUsd();
        }

        public int GetResultOfUsdFromConvertingBynToUsd()
        {
            return resultPage.GetResultOfUsdAfterConvertingFromBynToUsd();
        }

        public void FilteringUsingWithoutBaggageOptionOnResultPage()
        {
            resultPage.FilteringUsingWithoutBaggageOption();
        }

        public string GetBaggageAvailability()
        {
            return resultPage.GetBaggageAvailabilityResult();
        }

        public int ConvertingBynToEurOnResultPage()
        {
            return resultPage.ConvertFromBynToEur();
        }

        public int GetResultOfEurFromConvertingBynToEur()
        {
            return resultPage.GetResultOfUsdAfterConvertingFromBynToEur();
        }

        public int GetTheMostCheaperFlightPriceFromResultPage()
        {
            return resultPage.GetTheMostCheaperFlightPrice();
        }

        public int GetOtherOtherFlightPriceFromResultPage()
        {
            return resultPage.GetOtherFlightPrice();
        }

        public string CheckingForCorrectCountryOnBookingPage()
        {
            var bookingPage = resultPage.CheckingForCorrectCountryOnBookingOnBookingPage();

            bookingPage.BookingPageLoad();

            return bookingPage.CheckingForCorrectCountryOnBooking();
        }
    }
}
