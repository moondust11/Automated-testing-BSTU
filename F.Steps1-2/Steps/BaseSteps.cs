using OpenQA.Selenium;

namespace F.Steps1_2.Steps
{
    using DriverInstance = Driver.DriverInstance;

    public abstract class BaseSteps
    {
        protected IWebDriver Driver;

        public void InitBrowser()
        {
            Driver = DriverInstance.GetInstance();
        }

        public void CloseBrowser()
        {
            DriverInstance.CloseBrowser();
        }
    }
}
