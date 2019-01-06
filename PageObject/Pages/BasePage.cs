using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObject.Pages
{
    class BasePage
    {
        protected const string TestURL = "http://avia.bilet.by/";
        protected readonly IWebDriver Driver;

        public BasePage(IWebDriver webDriver)
        {
            PageFactory.InitElements(webDriver, this);
            Driver = webDriver;
        }
    }
}
