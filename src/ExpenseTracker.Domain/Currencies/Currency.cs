// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyIsoSymbol.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Currencies;

using Primitives;
using ValueObjects;

public class Currency : Entity<CurrencyId>
{
    public Currency(string isoSymbol, string symbol) : base(new CurrencyId(isoSymbol))
    {
        IsoSymbol = isoSymbol;
        Symbol = symbol;
    }

    public string IsoSymbol { get; private set; }
    public string Symbol { get; private set; }

    public override string ToString()
    {
        return $"{IsoSymbol} ({Symbol})";
    }
}