// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyId.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Currencies.ValueObjects;

using Primitives;

public class CurrencyId : ValueObject
{
    public CurrencyId(string isoSymbol)
    {
        IsoSymbol = isoSymbol;
    }

    public string IsoSymbol { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IsoSymbol;
    }
}