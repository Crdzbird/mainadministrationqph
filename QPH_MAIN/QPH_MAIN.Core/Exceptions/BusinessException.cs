using System;

namespace QPH_MAIN.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() {}
        public BusinessException(string message) : base(message) {}
    }
}