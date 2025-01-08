// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyFilterType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Currencies.Types;

using ExpenseTracker.Domain.Currencies;
using HotChocolate.Data.Filters;

/// <summary>
/// Defines the currency filter properties to expose.
/// </summary>
public class CurrencyFilterType : FilterInputType<Currency>
{
    /// <summary>
    /// Configures the currency filter type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IFilterInputTypeDescriptor<Currency> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.IsoSymbol);
    }
}