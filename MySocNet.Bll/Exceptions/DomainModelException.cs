using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Exceptions
{
    /// <summary>
    /// Violation of business logic rules (including data integrity constraints).
    /// In some cases caused by outer source (e.g. Database). For that purpose watch InnerException prop
    /// </summary>
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
