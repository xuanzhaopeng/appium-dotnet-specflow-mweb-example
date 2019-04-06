using NUnit.Framework;
using Spec.Web.Core.Config;
using OpenQA.Selenium.Appium.Enums;
using Moq;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;
using Spec.Web.Core.Enum;

namespace Spec.Web.Core.test
{
    [TestFixture(Category = "UnitTest")]
    class WebTestContextTest
    {
        private TimeoutConfig timeoutConfig = new TimeoutConfig
        {
            NewCommandTimeout = 90001,
            ImplicitWait = 90002,
            PageLoad = 90003
        };
        private LocalDriverConfig driverConfig;
        private Mock<DriverFactory> mockDriverFactory;
        private ICapabilities receivedCapabilities;
        private readonly string serverUrl = "http://localhost:4723/wd/hub";

        [SetUp]
        public void Setup()
        {
            mockDriverFactory = new Mock<DriverFactory>();
            mockDriverFactory.Setup(IDriverFactory =>
            IDriverFactory.Create(
                It.IsAny<Uri>(),
                It.IsAny<DriverOptions>(),
                It.IsAny<TimeSpan>(),
                It.IsAny<Action<IWebDriver>>()))
                .Callback((Uri serverUri, DriverOptions driverOptions, TimeSpan newCommandTimeout, Action<IWebDriver> callback) => receivedCapabilities = driverOptions.ToCapabilities())
                .Returns(default(Task<IWebDriver>));
        }

        [Test(Description = "create local android driver")]
        public void CreateLocalAndroidDriver()
        {
            driverConfig = new LocalDriverConfig
            {
                PlatformName = MobilePlatform.Android,
                PlatformVersion = "8",
                BrowserName = MobileBrowserType.Chrome,
                DeviceName = "device name",
                ServerUrl = serverUrl
            };
            var testSettings = new TestSettingsConfig();
            testSettings.Timeout = timeoutConfig;
            testSettings.LocalDriver = driverConfig;

            var webTestContext = new WebTestContext(mockDriverFactory.Object, testSettings);
            webTestContext.StartDriver();
            mockDriverFactory.Verify(IDriverFactory => IDriverFactory.Create(It.IsAny<Uri>(), It.IsAny<DriverOptions>(), It.IsAny<TimeSpan>(), It.IsAny<Action<IWebDriver>>()), Times.Once);
            Assert.AreEqual(driverConfig.BrowserName, receivedCapabilities.GetCapability(MobileCapabilityType.BrowserName), "BrowserName is not equal");
            Assert.AreEqual(driverConfig.PlatformVersion, receivedCapabilities.GetCapability(MobileCapabilityType.PlatformVersion), "PlatformVersion is not equal");
            Assert.AreEqual(driverConfig.PlatformName, receivedCapabilities.GetCapability(MobileCapabilityType.PlatformName), "PlatformVersion is not equal");
            Assert.AreEqual(driverConfig.DeviceName, receivedCapabilities.GetCapability(MobileCapabilityType.DeviceName), "DeviceName is not equal");
            Assert.AreEqual(ProviderType.Default, webTestContext.ProviderType);
        }

        [Test(Description = "create local iOS driver")]
        public void CreateLocalIOSDriver()
        {
            driverConfig = new LocalDriverConfig
            {
                PlatformName = MobilePlatform.IOS,
                PlatformVersion = "10.1",
                BrowserName = MobileBrowserType.Safari,
                DeviceName = "device name",
                ServerUrl = serverUrl
            };
            var testSettings = new TestSettingsConfig();
            testSettings.Timeout = timeoutConfig;
            testSettings.LocalDriver = driverConfig;

            var webTestContext = new WebTestContext(mockDriverFactory.Object, testSettings);
            webTestContext.StartDriver();
            mockDriverFactory.Verify(IDriverFactory => IDriverFactory.Create(It.IsAny<Uri>(), It.IsAny<DriverOptions>(), It.IsAny<TimeSpan>(), It.IsAny<Action<IWebDriver>>()), Times.Once);
            Assert.AreEqual(driverConfig.BrowserName, receivedCapabilities.GetCapability(MobileCapabilityType.BrowserName), "BrowserName is not equal");
            Assert.AreEqual(driverConfig.PlatformVersion, receivedCapabilities.GetCapability(MobileCapabilityType.PlatformVersion), "PlatformVersion is not equal");
            Assert.AreEqual(driverConfig.PlatformName, receivedCapabilities.GetCapability(MobileCapabilityType.PlatformName), "PlatformVersion is not equal");
            Assert.AreEqual(driverConfig.DeviceName, receivedCapabilities.GetCapability(MobileCapabilityType.DeviceName), "DeviceName is not equal");
            Assert.AreEqual(ProviderType.Default, webTestContext.ProviderType);
        }

        [Test(Description = "create local Remote web driver")]
        public void CreateLocalRemoteWebDriver()
        {
            driverConfig = new LocalDriverConfig
            {
                PlatformName = "",
                ServerUrl = serverUrl
            };
            var testSettings = new TestSettingsConfig();
            testSettings.Timeout = timeoutConfig;
            testSettings.LocalDriver = driverConfig;

            var webTestContext = new WebTestContext(mockDriverFactory.Object, testSettings);
            webTestContext.StartDriver();
            mockDriverFactory.Verify(IDriverFactory => IDriverFactory.Create(It.IsAny<Uri>(), It.IsAny<DriverOptions>(), It.IsAny<TimeSpan>(), It.IsAny<Action<IWebDriver>>()), Times.Once);
            Assert.AreEqual(ProviderType.Default, webTestContext.ProviderType);
        }
    }
}
