// -------------------------------------------------------------------------------------
//  <copyright file="DomainBasicValidationResult.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Shared;

public class DomainBasicValidationResult : DomainBasicResult
{
    public DomainBasicValidationResult(List<DomainError> errors)
        : base("Validation failed.")
    {
        Errors = errors;
    }

    public static DomainBasicValidationResult WithErrors(List<DomainError> errors)
    {
        return new DomainBasicValidationResult(errors);
    }
}