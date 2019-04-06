using OpenQA.Selenium;
using System;
using System.Runtime.Serialization;

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
        protected DriverNotStartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
