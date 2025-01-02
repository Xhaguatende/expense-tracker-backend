// -------------------------------------------------------------------------------------
//  <copyright file="Currency.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Expenses.ValueObjects;

using Primitives;

public class Currency : ValueObject
{
    public Currency(string isoSymbol, string symbol)
    {
        IsoSymbol = isoSymbol;
        Symbol = symbol;
    }

    public string IsoSymbol { get; set; }
    public string Symbol { get; set; }

    public override string ToString()
    {
        return $"{IsoSymbol} ({Symbol})";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IsoSymbol;
        yield return Symbol;
    }
}