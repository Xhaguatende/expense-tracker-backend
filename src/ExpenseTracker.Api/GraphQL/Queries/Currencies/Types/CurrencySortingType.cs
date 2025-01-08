// -------------------------------------------------------------------------------------
//  <copyright file="CurrencySortingType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Currencies.Types;

using ExpenseTracker.Domain.Currencies;
using HotChocolate.Data.Sorting;

/// <summary>
/// Defines the currency sorting properties to expose.
/// </summary>
public class CurrencySortingType : SortInputType<Currency>
{
    /// <summary>
    /// Configures the currency sorting type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(ISortInputTypeDescriptor<Currency> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.IsoSymbol);
    }
}