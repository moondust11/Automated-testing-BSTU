using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace SeleniumWebDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IWebDriver driver = new FirefoxDriver())
            {
                //Checking for correct source and destination cities

                string depCityConst = "Минск";
                string arrCityConst = "Тбилиси";
                int depDate = 26;
                int arrDate = 27;

                // Go to the site
                driver.Navigate().GoToUrl("http://avia.bilet.by");

                // Set the city of departure
                var inputFrom = driver.FindElement(By.Id("from_name"));
                inputFrom.Click();
                inputFrom.Clear();
                inputFrom.SendKeys(depCityConst);
                Thread.Sleep(2000);

                // Set the city of arrival
                var inputTo = driver.FindElement(By.Id("to_name"));
                inputTo.Click();
                inputTo.Clear();
                inputTo.SendKeys(arrCityConst);
                Thread.Sleep(2000);

                // Set departure date
                var dateFrom = driver.FindElement(By.Id("departure_date"));
                dateFrom.Click();
                Thread.Sleep(2000);
                var daysFrom = driver.FindElements(By.ClassName("ui-state-default"));
                daysFrom[depDate].Click();

                // Set arrival date
                var dateTo = driver.FindElement(By.Id("departure_date_1"));
                dateTo.Click();
                Thread.Sleep(2000);
                var daysTo = driver.FindElements(By.ClassName("ui-state-default"));
                daysTo[arrDate].Click();

                // Press searchButton
                IWebElement searchButton = driver.FindElement(By.XPath("//*[@id=\"page\"]/div[1]/div/div/div[1]/div[2]/form/div[2]/div/div[1]/div/input"));
                searchButton.Click();

                // Finding information by the system
                Thread.Sleep(10000);

                var depCityLi = driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[2]/li[3]"));
                var depCityLiSpan = driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[2]/li[3]/span[1]"));
                string depCity = depCityLi.Text.Replace(" " + depCityLiSpan.Text, "");

                var arrCityLi = driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[4]/li[3]"));
                var arrCityLiSpan = driver.FindElement(By.XPath("//*[@id=\"offers_table\"]/section[1]/div[1]/ul/li/label/span/span/ul[4]/li[3]/span[1]"));
                string arrCity = arrCityLi.Text.Replace(" " + arrCityLiSpan.Text, "");

                Assert.AreEqual(depCityConst, depCity);
                Assert.AreEqual(arrCityConst, arrCity);

                Thread.Sleep(5000);

                driver.Close();
            }
        }
    }
}
