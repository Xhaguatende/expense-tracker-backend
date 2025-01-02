// -------------------------------------------------------------------------------------
//  <copyright file="DomainValidationResult.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Shared;

public class DomainValidationResult<TValue> : DomainResult<TValue>
{
    public DomainValidationResult(TValue? value, List<DomainError> errors)
        : base(value, "Validation failed.")
    {
        Errors = errors;
    }

    public static DomainValidationResult<TValue> WithErrors(List<DomainError> errors)
    {
        return new DomainValidationResult<TValue>(default!, errors);
    }
}