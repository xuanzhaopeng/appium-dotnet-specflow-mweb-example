using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using Spec.Web.Core.Config;
using System;

namespace Spec.Web.Core.test.integration
{
    [TestFixture(Category = "IntegrationTest")]
    class WebTestContextIntegrationTest
    {
        private WebTestContext webTestContext;
        private DriverConfig driverConfig;
        private readonly string serverUrl = "http://localhost:4723/wd/hub";
        private TimeoutConfig timeoutConfig = new TimeoutConfig
        {
            NewCommandTimeout = 90001,
            ImplicitWait = 90002,
            PageLoad = 90003
        };


        [Test(Description = "start android driver")]
        public void StartAndroidDriver()
        {
            driverConfig = new DriverConfig
            {
                PlatformName = MobilePlatform.Android,
                BrowserName = MobileBrowserType.Chrome,
                DeviceName = "Android Emulator",
                ServerUrl = serverUrl
            };

            webTestContext = new WebTestContext(new DriverFactory(), driverConfig, timeoutConfig);
            webTestContext.StartDriver();
            Assert.IsInstanceOf<AndroidDriver<IWebElement>>(webTestContext.Driver());
        }

        [TearDown]
        public void TearDown()
        {
            if(webTestContext != null)
            {
                webTestContext.QuitDriver();
            }
        }
    }
}
