using System;

namespace Trill.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }
}