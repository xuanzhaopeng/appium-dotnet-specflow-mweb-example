using System;
using System.Configuration;

namespace Spec.Web.Core.Config
{
    public class TimeoutConfig : ConfigurationElement
    {
        [ConfigurationProperty("NewCommandTimeout", IsRequired =false, DefaultValue = (double)6000)]
        public double NewCommandTimeout
        {
            get { return (Double)this["NewCommandTimeout"]; }
            set { this["NewCommandTimeout"] = value; }
        }

        [ConfigurationProperty("ImplicitWait", IsRequired = false, DefaultValue = (double)6000)]
        public double ImplicitWait
        {
            get { return (Double)this["ImplicitWait"]; }
            set { this["ImplicitWait"] = value; }
        }

        [ConfigurationProperty("PageLoad", IsRequired = false, DefaultValue = (double)6000)]
        public double PageLoad
        {
            get { return (Double)this["PageLoad"]; }
            set { this["PageLoad"] = value; }
        }
    }
}
