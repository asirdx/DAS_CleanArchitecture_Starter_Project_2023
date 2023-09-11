using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Behaviors
{
    // global olarak validasyon kurallarını kontrol ettirip hata varsa bunları alıyoruz, yoksa bir sonraki middleware'e geçiyor.
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validators.Any()) // eğer validationlarda hata yoksa bir sonraki middleware işlemlerine geç
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errorDictionary = _validators
                .Select(s => s.Validate(context))
                .SelectMany(s => s.Errors)
                .Where(s => s != null)
                .GroupBy(
                s => s.PropertyName,
                s => s.ErrorMessage, (propertyName, errorMessage) => new
                {
                    Key = propertyName,
                    Values = errorMessage.Distinct().ToArray()
                })
                .ToDictionary(s => s.Key, s => s.Values[0]);

            if (errorDictionary.Any()) // hata varsa
            {
                var errors = errorDictionary.Select(s => new ValidationFailure
                {
                    PropertyName = s.Value,
                    ErrorCode = s.Key
                });
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}
