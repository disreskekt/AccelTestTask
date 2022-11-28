using System;

namespace AccelTestTask.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message)
        : base(message)
    {
    }
    
    public ValidationException()
        : base()
    {
    }
}