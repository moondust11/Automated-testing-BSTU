using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace F.Steps1_2.Pages
{
    public class BookingPage: BasePage
    {
        public BookingPage(IWebDriver driver) : base(driver)
        { }

        public void BookingPageLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.UrlContains("search/pre_booking"));
            Thread.Sleep(1000);
        }

        public string CheckingForCorrectCountryOnBooking()
        {
            return Driver.FindElement(By.XPath("//*[@id=\"citizenship_name_0_chosen\"]/a/span")).Text;
        }
    }
}
