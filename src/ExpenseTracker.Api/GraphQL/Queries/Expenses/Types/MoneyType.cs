// -------------------------------------------------------------------------------------
//  <copyright file="MoneyType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Expenses.Types;

using Domain.Expenses.ValueObjects;

/// <summary>
/// Defines the money type properties to expose.
/// </summary>
public class MoneyType : ObjectType<Money>
{
    /// <summary>
    /// Configures the money type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IObjectTypeDescriptor<Money> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Value);
        descriptor.Field(x => x.CurrencyIsoSymbol);
    }
}