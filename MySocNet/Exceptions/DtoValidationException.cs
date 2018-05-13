using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Exceptions
{
    /// <summary>
    /// Throw in case DTO does not have enough data, not all fields/props set (e.g. missing Login or Id)
    /// </summary>
    public class DtoValidationException : Exception
    {
        public DtoValidationException(string message):base(message)
        {
            
        }
        public DtoValidationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
