using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using Spec.Web.Core.Exceptions;

namespace Spec.Web.Core.Config
{
    public class BrowserStackDriverConfig : BaseDriverConfig
    {
        [ConfigurationProperty("User", IsRequired = false)]
        public string User
        {
            get { return (String)this["User"]; }
            set { this["User"] = value; }
        }

        [ConfigurationProperty("Key", IsRequired = false)]
        public string Key
        {
            get { return (String)this["Key"]; }
            set { this["Key"] = value; }
        }

        [ConfigurationProperty("Os", IsRequired = true)]
        public string Os
        {
            get { return (String)this["Os"]; }
            set { this["Os"] = value; }
        }

        [ConfigurationProperty("OsVersion", IsRequired = true)]
        public string OsVersion
        {
            get { return (String)this["OsVersion"]; }
            set { this["OsVersion"] = value; }
        }

        [ConfigurationProperty("Resolution", IsRequired = false, DefaultValue = "1024x768")]
        public string Resolution
        {
            get { return (String)this["Resolution"]; }
            set { this["Resolution"] = value; }
        }

        public override DriverOptions GetDriverOptions()
        {
            return GetDriverOptions(null, null, null, null);
        }

        public override DriverOptions GetDriverOptions(string platformName, string platformVersion, string deviceName, string browserName)
        {
            string _platformName = String.IsNullOrEmpty(platformName) ? PlatformName : platformName;
            string _browserName = String.IsNullOrEmpty(browserName) ? BrowserName : browserName;

            var _key = String.IsNullOrEmpty(Environment.GetEnvironmentVariable(Env.BrowserStackKey)) ? Key : Environment.GetEnvironmentVariable(Env.BrowserStackKey);
            var _user = String.IsNullOrEmpty(Environment.GetEnvironmentVariable(Env.BrowserStackUser)) ? User : Environment.GetEnvironmentVariable(Env.BrowserStackUser);

            switch (_platformName)
            {
                case MobilePlatform.Android:
                case MobilePlatform.IOS:
                    throw new DriverDoesNotSupportException("BrowserStack driver only support desktop browser test");
                default:
                    switch(_browserName)
                    {
                        case "Chrome":
                            var chromeOptions = new ChromeOptions();
                            chromeOptions.AddAdditionalCapability("browser_version", Version, true);
                            chromeOptions.AddAdditionalCapability("os", Os, true);
                            chromeOptions.AddAdditionalCapability("os_version", OsVersion, true);
                            chromeOptions.AddAdditionalCapability("resolution", Resolution, true);
                            chromeOptions.AddAdditionalCapability("browserstack.user", _user, true);
                            chromeOptions.AddAdditionalCapability("browserstack.key", _key, true);
                            return chromeOptions;
                        case "Firefox":
                            var firefoxOptions = new FirefoxOptions();
                            firefoxOptions.AddAdditionalCapability("browser_version", Version, true);
                            firefoxOptions.AddAdditionalCapability("os", Os, true);
                            firefoxOptions.AddAdditionalCapability("os_version", OsVersion, true);
                            firefoxOptions.AddAdditionalCapability("resolution", Resolution, true);
                            firefoxOptions.AddAdditionalCapability("browserstack.user", _user, true);
                            firefoxOptions.AddAdditionalCapability("browserstack.key", _key, true);
                            return firefoxOptions;
                        case "Safari":
                            var safariOptions = new SafariOptions();
                            safariOptions.AddAdditionalCapability("browser_version", Version);
                            safariOptions.AddAdditionalCapability("os", Os);
                            safariOptions.AddAdditionalCapability("os_version", OsVersion);
                            safariOptions.AddAdditionalCapability("resolution", Resolution);
                            safariOptions.AddAdditionalCapability("browserstack.user", _user);
                            safariOptions.AddAdditionalCapability("browserstack.key", _key);
                            return safariOptions;
                        default:
                            throw new BrowserDoesNotSupportException("Only support Chrome, Firefox and Safari");
                    }
            }
        }
    }
}
