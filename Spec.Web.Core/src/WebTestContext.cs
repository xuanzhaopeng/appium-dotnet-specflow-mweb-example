using NLog;
using OpenQA.Selenium;
using Spec.Web.Core.Config;
using Spec.Web.Core.Exceptions;
using Spec.Web.Core.Pages;
using Spec.Web.Core.Enum;
using System;
using System.Threading.Tasks;

namespace Spec.Web.Core
{
    public class WebTestContext
    {
        public ProviderType ProviderType { get; }

        public TestSettingsConfig TestSettings { get; }

        public T Driver<T>() where T : IWebDriver
        {
            CheckDriverStart();
            return (T)this._driver.Result;
        }

        public IWebDriver Driver()
        {
            CheckDriverStart();
            return this._driver.Result;
        }

        public WebTestContext(DriverFactory driverFactory, TestSettingsConfig testSettings)
        {
            logger.Info("============== Initializing Test context ==============");
            this.ProviderType = GetProviderType();
            this.TestSettings = testSettings;
            this.driverConfig = GetDriverConfig();

            this.serverUri = driverConfig.GetServerUri();
            this.driverOptions = driverConfig.GetDriverOptions();
            this.driverFactory = driverFactory;
            this.pageCollector = new PageCollector();
            logger.Info("- Driver Config: {0}", driverConfig.ToString());
            logger.Info("============== Initialized Test context complete ==============\n");
        }

        public void StartDriver()
        {
            logger.Info("============== Initializing Driver context ==============");
            logger.Info("- Capabilities: {0}", this.driverOptions.ToCapabilities().ToString());
            this._driver = this.driverFactory.Create(
                this.serverUri,
                this.driverOptions,
                TimeSpan.FromMilliseconds(this.driverConfig.GetNewCommandTimeout()),
                (IWebDriver driver) =>
                {
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(this.driverConfig.GetImplicitWait());
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(this.driverConfig.GetPageLoadTimeout());

                    logger.Info("Driver Timeout config:");
                    logger.Info("- Driver implicit wait: {0} milleseconds", this.driverConfig.GetImplicitWait());
                    logger.Info("- Driver page load wait: {0} milleseconds", this.driverConfig.GetPageLoadTimeout());
                    logger.Info("============== Initialized Driver Complete ==============\n");
                });
        }

        public void QuitDriver()
        {
            if (this._driver != null && this._driver.Result != null)
            {
                this._driver.Result.Quit();
            }
        }

        public PAGE GetPage<PAGE>() where PAGE : BasePage
        {
            return this.pageCollector.GetPage<PAGE>(Driver());
        }

        #region Private Methods
        private ProviderType GetProviderType()
        {
            try
            {
                return (ProviderType)System.Enum.Parse(typeof(ProviderType), Environment.GetEnvironmentVariable(Env.ServerProviderType), true);
            }
            catch (ArgumentNullException)
            {
                return ProviderType.Default;
            }
        }

        private void CheckDriverStart()
        {
            if (this._driver == null)
            {
                throw new DriverNotStartException("driver not start yet, please start driver at first");
            }
        }

        private IDriverConfig GetDriverConfig()
        {
            switch (this.ProviderType)
            {
                case ProviderType.SauceLabs:
                    return this.TestSettings.SauceLabsDriver;
                case ProviderType.BrowserStack:
                    return this.TestSettings.BrowserStackDriver;
                case ProviderType.Default:
                default:
                    return this.TestSettings.LocalDriver;
            }
        }

        private Task<IWebDriver> _driver;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly DriverOptions driverOptions;
        private readonly Uri serverUri;
        private readonly DriverFactory driverFactory;
        private readonly PageCollector pageCollector;
        private readonly IDriverConfig driverConfig;
        #endregion
    }
}
