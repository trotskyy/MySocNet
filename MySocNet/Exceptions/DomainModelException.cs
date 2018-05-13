using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Exceptions
{
    public class DomainModelException : Exception
    {
        public DomainModelException(string message):base(message)
        {

        }
        public DomainModelException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public DomainModelException(Exception innerException) : base("Watch inner exception", innerException)
        {

        }
    }
}
