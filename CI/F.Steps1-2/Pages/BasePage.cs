using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace F.Steps1_2.Pages
{
    public abstract class BasePage
    {
        protected readonly string BaseUrl = "http://avia.bilet.by/";
        protected readonly IWebDriver Driver;

        protected BasePage(IWebDriver webDriver)
        {
            PageFactory.InitElements(webDriver, this);
            Driver = webDriver;
        }
    }
}
