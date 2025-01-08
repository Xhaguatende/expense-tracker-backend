// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyNotFoundDomainError.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Currencies.Errors;

using Shared;

public class CurrencyNotFoundDomainError : DomainError
{
    public CurrencyNotFoundDomainError(string isoSymbol)
    : base(nameof(CurrencyNotFoundDomainError), $"Currency with ISO symbol '{isoSymbol}' was not found.")
    {
        IsoSymbol = isoSymbol;
    }

    public string IsoSymbol { get; }
}