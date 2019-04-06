using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using System;
using System.Configuration;

namespace Spec.Web.Core.Config
{
    public class SauceLabsDriverConfig : BaseDriverConfig
    {
        [ConfigurationProperty("ApiKey", IsRequired = false)]
        public string ApiKey
        {
            get { return (String)this["ApiKey"]; }
            set { this["ApiKey"] = value; }
        }

        [ConfigurationProperty("SessionCreationTimeout", IsRequired = false, DefaultValue = (Double)900000)]
        public double SessionCreationTimeout
        {
            get { return (Double)this["SessionCreationTimeout"]; }
            set { this["SessionCreationTimeout"] = value; }
        }

        [ConfigurationProperty("SuiteName", IsRequired = false)]
        public string SuiteName
        {
            get { return (String)this["SuiteName"]; }
            set { this["SuiteName"] = value; }
        }

        [ConfigurationProperty("AppiumVersion", IsRequired = false, DefaultValue = "1.12.0")]
        public string AppiumVersion
        {
            get { return (String)this["AppiumVersion"]; }
            set { this["AppiumVersion"] = value; }
        }

        public override DriverOptions GetDriverOptions()
        {
            return GetDriverOptions(null, null, null, null);
        }

        public override DriverOptions GetDriverOptions(string platformName, string platformVersion, string deviceName, string browserName)
        {
            var driverOptions = base.GetDriverOptions(platformName, platformVersion, deviceName, browserName);
            var apiKey = String.IsNullOrEmpty(Environment.GetEnvironmentVariable(Env.SauceLabsAPIKey)) ? ApiKey : Environment.GetEnvironmentVariable(Env.SauceLabsAPIKey);

            switch (driverOptions.ToCapabilities().GetCapability(MobileCapabilityType.PlatformName))
            {
                case MobilePlatform.Android:
                case MobilePlatform.IOS:
                    driverOptions.AddAdditionalCapability("testobject_api_key", apiKey);
                    driverOptions.AddAdditionalCapability("testobject_session_creation_timeout", SessionCreationTimeout);
                    driverOptions.AddAdditionalCapability("appiumVersion", AppiumVersion);
                    if(!String.IsNullOrEmpty(SuiteName))
                    {
                        driverOptions.AddAdditionalCapability("testobject_suite_name", SuiteName);
                    }
                    return driverOptions;
                default:
                    throw new Exception("SauceLabs driver only support Android and IOS");
            }
        }


    }
}
