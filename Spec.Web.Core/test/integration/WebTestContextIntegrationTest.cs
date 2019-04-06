using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using Spec.Web.Core.Config;

namespace Spec.Web.Core.test.integration
{
    [TestFixture(Category = "IntegrationTest")]
    class WebTestContextIntegrationTest
    {
        private WebTestContext webTestContext;
        private LocalDriverConfig driverConfig;


        [Test(Description = "start local android driver")]
        public void StartLocalAndroidDriver()
        {
            string serverUrl = "http://localhost:4723/wd/hub";
            driverConfig = new LocalDriverConfig
            {
                PlatformName = MobilePlatform.Android,
                BrowserName = MobileBrowserType.Chrome,
                DeviceName = "Android Emulator",
                ServerUrl = serverUrl,
                NewCommandTimeout = 90001,
                ImplicitWait = 90002,
                PageLoad = 90003
            };
            var testSettings = new TestSettingsConfig();
            testSettings.LocalDriver = driverConfig;

            webTestContext = new WebTestContext(new DriverFactory(), testSettings);
            webTestContext.StartDriver();
            Assert.IsInstanceOf<AndroidDriver<IWebElement>>(webTestContext.Driver());
        }

        [Test(Description = "start local chrome driver")]
        public void StartLocalChromeDriver()
        {
            string serverUrl = "http://localhost:4444/wd/hub";
            driverConfig = new LocalDriverConfig
            {
                BrowserName = MobileBrowserType.Chrome,
                ServerUrl = serverUrl,
                NewCommandTimeout = 90001,
                ImplicitWait = 90002,
                PageLoad = 90003
            };
            var testSettings = new TestSettingsConfig();
            testSettings.LocalDriver = driverConfig;

            webTestContext = new WebTestContext(new DriverFactory(), testSettings);
            webTestContext.StartDriver();
            Assert.IsInstanceOf<RemoteWebDriver>(webTestContext.Driver());
        }

        [TearDown]
        public void TearDown()
        {
            if (webTestContext != null)
            {
                webTestContext.QuitDriver();
            }
        }
    }
}
