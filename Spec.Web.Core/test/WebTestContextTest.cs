using NUnit.Framework;
using Spec.Web.Core.Config;
using OpenQA.Selenium.Appium.Enums;
using Moq;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;

namespace Spec.Web.Core.test
{
    [TestFixture]
    class WebTestContextTest
    {
        private TimeoutConfig timeoutConfig = new TimeoutConfig
        {
            NewCommandTimeout = 90001,
            ImplicitWait = 90002,
            PageLoad = 90003
        };
        private DriverConfig driverConfig;
        private Mock<DriverFactory> mockDriverFactory;
        private ICapabilities receivedCapabilities;
        private readonly string serverUrl = "http://localhost:4723/wd/hub";

        [SetUp]
        public void Setup()
        {
            mockDriverFactory = new Mock<DriverFactory>();
            mockDriverFactory.Setup(IDriverFactory =>
            IDriverFactory.Create(
                It.IsAny<String>(),
                It.IsAny<Uri>(),
                It.IsAny<DriverOptions>(),
                It.IsAny<TimeSpan>(),
                It.IsAny<Action<IWebDriver>>()))
                .Callback((string platform, Uri serverUri, DriverOptions driverOptions, TimeSpan newCommandTimeout, Action<IWebDriver> callback) => receivedCapabilities = driverOptions.ToCapabilities())
                .Returns(default(Task<IWebDriver>));
        }

        [Test(Description = "create android driver")]
        public void StartAndroidDriver()
        {
            driverConfig = new DriverConfig
            {
                PlatformName = MobilePlatform.Android,
                PlatformVersion = "8",
                BrowserName = MobileBrowserType.Chrome,
                DeviceName = "device name",
                ServerUrl = serverUrl
            };

            var webTestContext = new WebTestContext(mockDriverFactory.Object, driverConfig, timeoutConfig);
            webTestContext.StartDriver();
            mockDriverFactory.Verify(IDriverFactory => IDriverFactory.Create(MobilePlatform.Android, It.IsAny<Uri>(), It.IsAny<DriverOptions>(), It.IsAny<TimeSpan>(), It.IsAny<Action<IWebDriver>>()), Times.Once);
            Assert.AreEqual(driverConfig.BrowserName, receivedCapabilities.GetCapability(MobileCapabilityType.BrowserName), "BrowserName is not equal");
            Assert.AreEqual(driverConfig.PlatformVersion, receivedCapabilities.GetCapability(MobileCapabilityType.PlatformVersion), "PlatformVersion is not equal");
            Assert.AreEqual(driverConfig.PlatformVersion, receivedCapabilities.GetCapability(MobileCapabilityType.PlatformVersion), "PlatformVersion is not equal");
            Assert.AreEqual(driverConfig.DeviceName, receivedCapabilities.GetCapability(MobileCapabilityType.DeviceName), "DeviceName is not equal");
        }

        [Test(Description = "create iOS driver")]
        public void StartIOSDriver()
        {
            driverConfig = new DriverConfig
            {
                PlatformName = MobilePlatform.IOS,
                PlatformVersion = "10.1",
                BrowserName = MobileBrowserType.Safari,
                DeviceName = "device name",
                ServerUrl = serverUrl
            };

            var webTestContext = new WebTestContext(mockDriverFactory.Object, driverConfig, timeoutConfig);
            webTestContext.StartDriver();
            mockDriverFactory.Verify(IDriverFactory => IDriverFactory.Create(MobilePlatform.IOS, It.IsAny<Uri>(), It.IsAny<DriverOptions>(), It.IsAny<TimeSpan>(), It.IsAny<Action<IWebDriver>>()), Times.Once);
            Assert.AreEqual(driverConfig.BrowserName, receivedCapabilities.GetCapability(MobileCapabilityType.BrowserName), "BrowserName is not equal");
            Assert.AreEqual(driverConfig.PlatformVersion, receivedCapabilities.GetCapability(MobileCapabilityType.PlatformVersion), "PlatformVersion is not equal");
            Assert.AreEqual(driverConfig.PlatformVersion, receivedCapabilities.GetCapability(MobileCapabilityType.PlatformVersion), "PlatformVersion is not equal");
            Assert.AreEqual(driverConfig.DeviceName, receivedCapabilities.GetCapability(MobileCapabilityType.DeviceName), "DeviceName is not equal");
        }

        [Test(Description = "create Remote web driver")]
        public void StartRemoteWebDriver()
        {
            driverConfig = new DriverConfig
            {
                PlatformName = "",
                ServerUrl = serverUrl
            };

            var webTestContext = new WebTestContext(mockDriverFactory.Object, driverConfig, timeoutConfig);
            webTestContext.StartDriver();
            mockDriverFactory.Verify(IDriverFactory => IDriverFactory.Create("", It.IsAny<Uri>(), It.IsAny<DriverOptions>(), It.IsAny<TimeSpan>(), It.IsAny<Action<IWebDriver>>()), Times.Once);
        }
    }
}
