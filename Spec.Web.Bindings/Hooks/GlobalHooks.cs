using TechTalk.SpecFlow;
using Spec.Web.Core;
using Spec.Web.Core.Config;
using Spec.Web.Core.Specflow;
using System.Configuration;
using NLog;
using System;

namespace Spec.Web.Bindings.Hooks
{
    public sealed class GlobalHooks
    {
        [Binding]
        public sealed class GlobalHook
        {
            private static DriverFactory driverFactory;
            private static TestSettingsConfig testSettings;
            private static Logger logger = LogManager.GetCurrentClassLogger();

            [BeforeTestRun]
            public static void BeforeTestRun()
            {
                driverFactory = new DriverFactory();
                testSettings = ConfigurationManager.GetSection(ConfigSectionTypes.TestingSettings) as TestSettingsConfig;
            }

            [BeforeScenario]
            public void BeforeScenario()
            {
                logger.Info(String.Format("Working directory: {0}", Environment.CurrentDirectory));
                WebTestContext webTestContext = new WebTestContext(driverFactory, testSettings);
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
}
