using System.Configuration;

namespace Spec.Web.Core.Config
{
    public class TestSettingsConfig : ConfigurationSection
    {
        [ConfigurationProperty("Driver", IsRequired = true)]
        public DriverConfig Driver
        {
            get { return (DriverConfig)this["Driver"]; }
            set { this["Driver"] = value; }
        }

        [ConfigurationProperty("Timeout")]
        public TimeoutConfig Timeout
        {
            get { return (TimeoutConfig)this["Timeout"]; }
            set { this["Timeout"] = value; }
        }
    }
}
