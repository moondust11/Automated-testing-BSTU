using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;

namespace F.Steps1_2.Pages
{
    public class SearchFormPage: BasePage
    {
        [FindsBy(How = How.Id, Using = "from_name")]
        private IWebElement inputFrom;

        [FindsBy(How = How.Id, Using = "to_name")]
        private IWebElement inputTo;

        [FindsBy(How = How.Id, Using = "departure_date")]
        private IWebElement dateFrom;

        [FindsBy(How = How.Id, Using = "departure_date_1")]
        private IWebElement dateTo;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"page\"]/div[1]/div/div/div[1]/div[2]/form/div[2]/div/div[1]/div/input")]
        private IWebElement searchButton;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"plus_minus_chosen\"]")]
        private IWebElement exactDateFrom;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"plus_minus_1_chosen\"]")]
        private IWebElement exactDateTo;

        public SearchFormPage(IWebDriver webDriver) : base(webDriver)
        { }

        public void Navigate()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
        }

        public void SetCitiesAndDays(string from, string to, int dayFrom, int dayTo)
        {
            inputFrom.Click();
            inputFrom.Clear();
            inputFrom.SendKeys(from);
            Thread.Sleep(4000);

            inputTo.Click();
            inputTo.Clear();
            inputTo.SendKeys(to);
            Thread.Sleep(4000);

            dateFrom.Click();
            Thread.Sleep(1000);
            var daysFromElement = Driver.FindElements(By.ClassName("ui-state-default"));
            daysFromElement[dayFrom].Click();

            dateTo.Click();
            Thread.Sleep(1000);
            var daysToElement = Driver.FindElements(By.ClassName("ui-state-default"));
            daysToElement[dayTo].Click();
        }

        public void SetExactDateOptions()
        {
            exactDateFrom.Click();
            Thread.Sleep(2000);
            var exactDateFromOption = Driver.FindElement(By.XPath("//*[@id=\"plus_minus_chosen\"]/div/ul/li[1]"));
            exactDateFromOption.Click();

            Thread.Sleep(1000);

            exactDateTo.Click();
            Thread.Sleep(2000);
            var exactDateToOption = Driver.FindElement(By.XPath("//*[@id=\"plus_minus_1_chosen\"]/div/ul/li[1]"));
            exactDateToOption.Click();
        }

        public ResultFormPage SearchToResultPage()
        {
            searchButton.Click();

            return new ResultFormPage(Driver);
        }
    }
}
