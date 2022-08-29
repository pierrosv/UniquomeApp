using FluentValidation;
using FluentValidation.Results;
using MediatR;
using UniquomeApp.Application.Common.Exceptions;

namespace UniquomeApp.Application;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any()) return await next();
        var context = new ValidationContext<TRequest>(request);

        var failures2 = new List<ValidationResult>();
        foreach (var v in _validators)
        {
            var f = await v.ValidateAsync(context);
            if (f != null) failures2.Add(f);
        }

        var failures =
            failures2.SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();
        //
        // var failures = _validators
        //     .Select(v => v.Validate(context))
        //     .SelectMany(result => result.Errors)
        //     .Where(f => f != null)
        //     .ToList();

        if (failures.Count != 0)
            throw new EntityValidationException(failures);

        return await next();
    }
}