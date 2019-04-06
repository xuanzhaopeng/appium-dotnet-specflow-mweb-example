using TechTalk.SpecFlow;
using Spec.Web.Core;
using Spec.Web.Core.Config;
using Spec.Web.Core.Specflow;
using System.Configuration;
using NLog;

namespace Spec.Web.Android.Hooks
{
    [Binding]
    public sealed class GlobalHook
    {
        private static DriverFactory driverFactory;
        private static DriverConfig driverConfig;
        private static TimeoutConfig timeoutConfig;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            driverFactory = new DriverFactory();
            TestSettingsConfig testSettings = ConfigurationManager.GetSection("TestSettings") as TestSettingsConfig;
            driverConfig = testSettings.Driver;
            timeoutConfig = testSettings.Timeout;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            WebTestContext webTestContext = new WebTestContext(driverFactory, driverConfig, timeoutConfig);
            webTestContext.StartDriver();
            ScenarioContext.Current.Add(ScenarioContextTypes.Driver, webTestContext);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.ContainsKey(ScenarioContextTypes.Driver))
            {
                WebTestContext testContext = ScenarioContext.Current.Get<WebTestContext>(ScenarioContextTypes.Driver);
                testContext.QuitDriver();
            }
        }
    }
}
