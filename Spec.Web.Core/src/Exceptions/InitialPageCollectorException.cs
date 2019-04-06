using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spec.Web.Core.Exceptions
{
    public class InitialPageCollectorException : WebDriverException
    {
        public InitialPageCollectorException() : base()
        {
        }
        public InitialPageCollectorException(string message) : base(message)
        {
        }
        public InitialPageCollectorException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected InitialPageCollectorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
