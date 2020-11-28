using System;

namespace Trill.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        protected AppException(string message) : base(message)
        {
        }
    }
}