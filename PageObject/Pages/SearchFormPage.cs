using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PageObject.Pages
{
    class SearchFormPage: BasePage
    {
        public SearchFormPage(IWebDriver webDriver) : base(webDriver)
        { }

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

        public void Navigate()
        {
            Driver.Navigate().GoToUrl(TestURL);
        }

        public ResultFormPage SearchToResultPage(string from, string to, int dayFrom, int dayTo)
        {
            inputFrom.Click();
            inputFrom.Clear();
            inputFrom.SendKeys(from);
            Thread.Sleep(3000);

            inputTo.Click();
            inputTo.Clear();
            inputTo.SendKeys(to);
            Thread.Sleep(3000);

            dateFrom.Click();
            Thread.Sleep(2000);
            var daysFromElement = Driver.FindElements(By.ClassName("ui-state-default"));
            daysFromElement[dayFrom].Click();

            dateTo.Click();
            Thread.Sleep(2000);
            var daysToElement = Driver.FindElements(By.ClassName("ui-state-default"));
            daysToElement[dayTo].Click();

            searchButton.Click();

            return new ResultFormPage(Driver);
        }
    }
}
