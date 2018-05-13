using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Exceptions
{
    public class IdNotSpecifiedException : DtoValidationException
    {
        public IdNotSpecifiedException() : base("Id not specified")
        {
        }
    }
}
