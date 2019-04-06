

using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;

namespace Spec.Web.Core.Config
{
    public abstract class BaseDriverConfig : ConfigurationElement, IDriverConfig
    {
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

        public virtual DriverOptions GetDriverOptions(double newCommandTimeout)
        {
            return GetDriverOptions(null, null, null, null, newCommandTimeout);
        }

        public virtual DriverOptions GetDriverOptions(string platformName, string platformVersion, string deviceName, string browserName, double newCommandTimeout)
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
                    driverOptions.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, newCommandTimeout);
                    return driverOptions;
                default:
                    return new ChromeOptions();
            }
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
