using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Application.Exceptions
{
    public class ValidationExceptions: Exception
    {
        public ValidationExceptions(): base("Se han producido uno o más errores de validación") 
        {
            Errors = new List<string>();        
        }

        public List<string> Errors { get; private set; }

        public ValidationExceptions(IEnumerable<ValidationFailure> failures): this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
            
        }
    }
}
