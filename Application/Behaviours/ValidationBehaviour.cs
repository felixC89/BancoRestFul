using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviours
{
    public class ValidationBehaviour<TResquest, TResponse> : IPipelineBehavior<TResquest, TResponse> where TResquest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TResquest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TResquest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TResquest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any()) 
            {
                var context = new FluentValidation.ValidationContext<TResquest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0) 
                {
                    throw new Exceptions.ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
