using OpenQA.Selenium;
using System;

namespace Spec.Web.Core.Exceptions
{
    public class DriverDoesNotSupportException : WebDriverException
    {
        public DriverDoesNotSupportException() : base()
        {
        }
        public DriverDoesNotSupportException(string message) : base(message)
        {
        }
        public DriverDoesNotSupportException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
