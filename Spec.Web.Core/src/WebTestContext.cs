using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using Spec.Web.Core.Config;
using Spec.Web.Core.Exceptions;
using Spec.Web.Core.Pages;
using System;
using System.Threading.Tasks;

namespace Spec.Web.Core
{
    public class WebTestContext
    {
        public string Platform { get; }
        public string PlatformVersion { get; }
        public string BrowserName { get; }

        public T Driver<T>() where T : IWebDriver
        {
            CheckDriverStart();
            return (T)this._Driver.Result;
        }

        public IWebDriver Driver()
        {
            CheckDriverStart();
            return this._Driver.Result;
        }

        public WebTestContext(DriverFactory driverFactory, DriverConfig driverConfig, TimeoutConfig timeoutConfig)
        {
            this.ServerUri = new Uri(driverConfig.ServerUrl);
            this.TimeoutConfig = timeoutConfig;
            this.Platform = driverConfig.PlatformName;
            this.PlatformVersion = driverConfig.PlatformVersion;
            this.BrowserName = driverConfig.BrowserName;
            this.DriverOptions = CreateDriverOptions(driverConfig);
            this.DriverFactory = driverFactory;
            this.PageCollector = new PageCollector();
            logger.Debug("============== Initialize Test context ==============");
            logger.Debug("- Driver Config: \n{0}", driverConfig.ToString());
        }

        public void StartDriver()
        {
            this._Driver = this.DriverFactory.Create(
                this.Platform, 
                this.ServerUri, 
                this.DriverOptions, 
                TimeSpan.FromMilliseconds(this.TimeoutConfig.NewCommandTimeout),
                (IWebDriver driver) => {
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(this.TimeoutConfig.ImplicitWait);
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(this.TimeoutConfig.PageLoad);
                });
        }

        public void QuitDriver()
        {
            if(this._Driver != null && this._Driver.Result != null)
            {
                this._Driver.Result.Quit();
            }
        }

        public PAGE GetPage<PAGE>() where PAGE : BasePage
        {
            return this.PageCollector.GetPage<PAGE>(Driver());
        }

        #region Private Methods
        private void CheckDriverStart()
        {
            if(this._Driver == null)
            {
                throw new DriverNotStartException("driver not start yet, please start driver at first");
            }
        }

        private DriverOptions CreateDriverOptions(DriverConfig driverConfig)
        {
            switch (driverConfig.PlatformName)
            {
                case MobilePlatform.Android:
                case MobilePlatform.IOS:
                    var driverOptions = new AppiumOptions();
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, driverConfig.BrowserName);
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, driverConfig.DeviceName);
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, driverConfig.PlatformVersion);
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, this.TimeoutConfig.NewCommandTimeout);
                    return driverOptions;
                default:
                    return new ChromeOptions();
            }
        }

        private Task<IWebDriver> _Driver;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly DriverOptions DriverOptions;
        private readonly Uri ServerUri;
        private readonly TimeoutConfig TimeoutConfig;
        private readonly DriverFactory DriverFactory;
        private PageCollector PageCollector;
        #endregion
    }
}
