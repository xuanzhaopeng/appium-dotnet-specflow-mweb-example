using OpenQA.Selenium;
using System;
using System.Runtime.Serialization;

namespace Spec.Web.Core.Exceptions
{
    public class IncorrectServerProviderTypeException : WebDriverException
    {
        public IncorrectServerProviderTypeException() : base()
        {
        }
        public IncorrectServerProviderTypeException(string message) : base(message)
        {
        }
        public IncorrectServerProviderTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected IncorrectServerProviderTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
