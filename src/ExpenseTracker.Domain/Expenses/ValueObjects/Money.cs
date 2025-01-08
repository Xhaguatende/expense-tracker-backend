// -------------------------------------------------------------------------------------
//  <copyright file="Money.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Expenses.ValueObjects;

using Primitives;

public class Money : ValueObject
{
    public required string CurrencyIsoSymbol { get; set; }
    public required decimal Value { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return CurrencyIsoSymbol;
    }
}