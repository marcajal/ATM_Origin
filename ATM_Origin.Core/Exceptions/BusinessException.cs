using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Origin.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }

        public BusinessException( string message) : base(message)
        {

        }

    }
}
