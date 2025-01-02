// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyNotFoundDomainError.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Expenses.Errors;

using Shared;

public class CurrencyNotFoundDomainError : DomainError
{
    public CurrencyNotFoundDomainError(string isoCurrencySymbol)
    : base(nameof(CurrencyNotFoundDomainError), $"Currency with ISO symbol '{isoCurrencySymbol}' was not found.")
    {
        IsoCurrencySymbol = isoCurrencySymbol;
    }

    public string IsoCurrencySymbol { get; }
}