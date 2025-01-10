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
    public Currency(string isoSymbol, string name) : base(new CurrencyId(isoSymbol))
    {
        IsoSymbol = isoSymbol;
        Name = name;
    }

    public string IsoSymbol { get; private set; }
    public string Name { get; private set; }

    public override string ToString()
    {
        return $"{IsoSymbol} ({Name})";
    }
}