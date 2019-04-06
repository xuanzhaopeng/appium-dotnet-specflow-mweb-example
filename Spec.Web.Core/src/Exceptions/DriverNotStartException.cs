using OpenQA.Selenium;
using System;

namespace Spec.Web.Core.Exceptions
{
    public class DriverNotStartException : WebDriverException
    {
        public DriverNotStartException() : base()
        {
        }
        public DriverNotStartException(string message) : base(message)
        {
        }
        public DriverNotStartException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
