using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class ValidationException: Exception
    {
        public List<string> Errors { get; private set; }
        protected ValidationException(SerializationInfo info, StreamingContext context)
        {
            Console.WriteLine(info);
        }
        public ValidationException(): base("Se han producido uno o más errores de validación") 
        {
            Errors = new List<string>();        
        }
        public ValidationException(IEnumerable<ValidationFailure> failures): this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
            
        }

    }
}
