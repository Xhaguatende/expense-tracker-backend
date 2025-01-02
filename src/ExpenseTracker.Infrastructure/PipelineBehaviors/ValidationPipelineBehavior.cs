// -------------------------------------------------------------------------------------
//  <copyright file="ValidationPipelineBehavior.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.PipelineBehaviors;

using Domain.Shared;
using FluentValidation;
using MediatR;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : DomainBasicResult
{

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var errors = await GetErrorsAsync(request, cancellationToken);

        if (errors.Count == 0)
        {
            return await next();
        }

        return CreateValidationResult(errors);
    }

    private static TResponse CreateValidationResult(List<DomainError> errors)
    {
        if (typeof(TResponse) == typeof(DomainBasicResult))
        {
            return (DomainBasicValidationResult.WithErrors(errors) as TResponse)!;
        }

        var result = typeof(DomainValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResponse).GenericTypeArguments[0])
            .GetMethod(nameof(DomainBasicValidationResult.WithErrors))!
            .Invoke(null, [errors])!;

        return (TResponse)result;
    }
    private async Task<List<DomainError>> GetErrorsAsync(TRequest request, CancellationToken cancellationToken)
    {
        var errors = new List<DomainError>();

        foreach (var validator in _validators)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult is null)
            {
                continue;
            }

            var validationFailures = validationResult.Errors.Where(x => x is not null);

            errors.AddRange(
                validationFailures.Select(
                    validationFailure => new DomainError(
                        validationFailure.ErrorCode,
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage)));
        }

        return errors;
    }
}