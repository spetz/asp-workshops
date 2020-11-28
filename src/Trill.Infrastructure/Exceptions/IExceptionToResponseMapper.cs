using System;

namespace Trill.Infrastructure.Exceptions
{
    internal interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}