

using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using Spec.Web.Core.Exceptions;
using System;
using System.Configuration;

namespace Spec.Web.Core.Config
{
    public abstract class BaseDriverConfig : ConfigurationElement, IDriverConfig
    {
        [ConfigurationProperty("Platform", IsRequired = false, DefaultValue = "")]
        public string Platform
        {
            get { return (String)this["Platform"]; }
            set { this["Platform"] = value; }
        }

        [ConfigurationProperty("Version", IsRequired = false, DefaultValue = "")]
        public string Version
        {
            get { return (String)this["Version"]; }
            set { this["Version"] = value; }
        }

        [ConfigurationProperty("PlatformName", IsRequired = false, DefaultValue = "")]
        public string PlatformName
        {
            get { return (String)this["PlatformName"]; }
            set { this["PlatformName"] = value; }
        }

        [ConfigurationProperty("PlatformVersion", IsRequired = false, DefaultValue = "")]
        public string PlatformVersion
        {
            get { return (String)this["PlatformVersion"]; }
            set { this["PlatformVersion"] = value; }
        }

        [ConfigurationProperty("BrowserName", IsRequired = true)]
        public string BrowserName
        {
            get { return (String)this["BrowserName"]; }
            set { this["BrowserName"] = value; }
        }

        [ConfigurationProperty("DeviceName", IsRequired = false, DefaultValue = "")]
        public string DeviceName
        {
            get { return (String)this["DeviceName"]; }
            set { this["DeviceName"] = value; }
        }

        [ConfigurationProperty("ServerUrl", IsRequired = true)]
        public string ServerUrl
        {
            get { return (String)this["ServerUrl"]; }
            set { this["ServerUrl"] = value; }
        }

        [ConfigurationProperty("NewCommandTimeout", IsRequired = false, DefaultValue = (double)60000)]
        public double NewCommandTimeout
        {
            get { return (Double)this["NewCommandTimeout"]; }
            set { this["NewCommandTimeout"] = value; }
        }

        [ConfigurationProperty("ImplicitWait", IsRequired = false, DefaultValue = (double)60000)]
        public double ImplicitWait
        {
            get { return (Double)this["ImplicitWait"]; }
            set { this["ImplicitWait"] = value; }
        }

        [ConfigurationProperty("PageLoad", IsRequired = false, DefaultValue = (double)60000)]
        public double PageLoad
        {
            get { return (Double)this["PageLoad"]; }
            set { this["PageLoad"] = value; }
        }

        public virtual DriverOptions GetDriverOptions()
        {
            return GetDriverOptions(null, null, null, null);
        }

        public virtual DriverOptions GetDriverOptions(string platformName, string platformVersion, string deviceName, string browserName)
        {
            string _platformName = String.IsNullOrEmpty(platformName) ? PlatformName : platformName;
            string _platformVersion = String.IsNullOrEmpty(platformVersion) ? PlatformVersion : platformVersion;
            string _deviceName = String.IsNullOrEmpty(deviceName) ? DeviceName : deviceName;
            string _browserName = String.IsNullOrEmpty(browserName) ? BrowserName : browserName;

            switch (_platformName)
            {
                case MobilePlatform.Android:
                case MobilePlatform.IOS:
                    var driverOptions = new AppiumOptions();
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, _browserName);
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, _deviceName);
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, _platformVersion);
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, _platformName);
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, NewCommandTimeout);
                    return driverOptions;
                default:
                    switch (_browserName)
                    {
                        case "Chrome":
                            return new ChromeOptions();
                        case "Firefox":
                            return new FirefoxOptions();
                        case "Safari":
                            return new SafariOptions();
                        default:
                            throw new BrowserDoesNotSupportException("Only support Chrome, Firefox and Safari");
                    }
            }
        }

        public double GetImplicitWait()
        {
            return ImplicitWait;
        }

        public double GetNewCommandTimeout()
        {
            return NewCommandTimeout;
        }

        public double GetPageLoadTimeout()
        {
            return PageLoad;
        }

        public Uri GetServerUri()
        {
            return new Uri(ServerUrl);
        }

        public override string ToString()
        {
            string pattern = @"---------------------
    PlatformName:    {0}
    PlatformVersion: {1}
    BrowserName:     {2}
    DeviceName:      {3}
    ServerUrl:       {4}
---------------------";
            return String.Format(pattern, this["PlatformName"], this["PlatformVersion"], this["BrowserName"], this["DeviceName"], this["ServerUrl"]);
        }

    }
}
