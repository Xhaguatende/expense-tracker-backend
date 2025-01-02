// -------------------------------------------------------------------------------------
//  <copyright file="DomainResult.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Shared;

public class DomainResult<TValue> : DomainBasicResult
{
    public DomainResult(
        TValue? value,
        string? errorTitle = null,
        List<DomainError>? errors = null)
        : base(errorTitle, errors)
    {
        Value = value;
    }

    public TValue? Value { get; }

    public static DomainResult<TValue> Failure(DomainError error, string? errorTitle = null)
    {
        return new DomainResult<TValue>(
            default!,
            string.IsNullOrWhiteSpace(errorTitle) ? "An error occurred." : errorTitle,
            [error]);
    }

    public static DomainResult<TValue> Failure(List<DomainError> errors, string? errorTitle = null)
    {
        return new DomainResult<TValue>(
            default!,
            string.IsNullOrWhiteSpace(errorTitle) ? "An error occurred." : errorTitle,
            errors);
    }

    public static implicit operator DomainResult<TValue>(TValue value)
    {
        return new DomainResult<TValue>(value);
    }
}