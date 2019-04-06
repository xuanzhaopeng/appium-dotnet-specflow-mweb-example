using OpenQA.Selenium;
using System;

namespace Spec.Web.Core.Exceptions
{
    public class BrowserDoesNotSupportException : WebDriverException
    {
        public BrowserDoesNotSupportException() : base()
        {
        }
        public BrowserDoesNotSupportException(string message) : base(message)
        {
        }
        public BrowserDoesNotSupportException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
