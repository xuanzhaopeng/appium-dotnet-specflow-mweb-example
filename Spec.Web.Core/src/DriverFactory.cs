using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Threading.Tasks;

namespace Spec.Web.Core
{
    public class DriverFactory
    {
        public DriverFactory() { }

        public virtual Task<IWebDriver> Create(Uri serverUri, DriverOptions driverOptions, TimeSpan newCommandTimeout, Action<IWebDriver> callback)
        {
            Console.Out.WriteLine(driverOptions.ToString());
            switch (driverOptions.ToCapabilities().GetCapability(MobileCapabilityType.PlatformName))
            {
                case MobilePlatform.Android:
                    return CreateAndroidDriver(serverUri, driverOptions, newCommandTimeout, callback);
                case MobilePlatform.IOS:
                    return CreateIOSDriver(serverUri, driverOptions, newCommandTimeout, callback);
                default:
                    return CreateRemoteWebDriver(serverUri, driverOptions, newCommandTimeout, callback);
            }
        }

        protected Task<IWebDriver> CreateAndroidDriver(Uri serverUri, DriverOptions driverOptions, TimeSpan newCommandTimeout, Action<IWebDriver> callback)
        {
            logger.Info("===> Create Android Driver");
            return Task.Run<IWebDriver>(() =>
            {
                var driver = new AndroidDriver<IWebElement>(serverUri, driverOptions, newCommandTimeout);
                callback(driver);
                return driver;
            });
        }

        protected Task<IWebDriver> CreateIOSDriver(Uri serverUri, DriverOptions driverOptions, TimeSpan newCommandTimeout, Action<IWebDriver> callback)
        {
            logger.Info("===> Create IOS Driver");
            return Task.Run<IWebDriver>(() =>
            {
                var driver = new IOSDriver<IWebElement>(serverUri, driverOptions, newCommandTimeout);
                callback(driver);
                return driver;
            });
        }

        protected Task<IWebDriver> CreateRemoteWebDriver(Uri serverUri, DriverOptions driverOptions, TimeSpan newCommandTimeout, Action<IWebDriver> callback)
        {
            logger.Info("===> Create Remote Web Driver");
            return Task.Run<IWebDriver>(() =>
            {
                var driver = new RemoteWebDriver(serverUri, driverOptions.ToCapabilities(), newCommandTimeout);
                callback(driver);
                return driver;
            });
        }

        private readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}
