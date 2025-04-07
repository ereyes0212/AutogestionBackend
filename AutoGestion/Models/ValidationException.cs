using System;
using System.Collections.Generic;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("Se han producido uno o más errores de validación.")
    {
        Errors = errors;
    }
}
