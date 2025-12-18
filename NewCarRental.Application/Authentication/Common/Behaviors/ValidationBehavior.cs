using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace NewCarRental.Application.Authentication.Common.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return await next();

            // Create context for validate
            var context = new ValidationContext<TRequest>(request);

            // Run all validator found
            var validationResult = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResult
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            // if have any error, throw Exception 
            if (failures.Any()) throw new ValidationException(failures);

            return await next();
        }
    }
}
