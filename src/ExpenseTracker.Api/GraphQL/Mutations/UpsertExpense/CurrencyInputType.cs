// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyInputType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Mutations.UpsertExpense;

using Domain.Expenses.ValueObjects;

/// <summary>
/// Represents the input type for the currency value object.
/// </summary>
public class CurrencyInputType : InputObjectType<Currency>
{
    /// <summary>
    /// Configures the money input type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IInputObjectTypeDescriptor<Currency> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.IsoSymbol)
            .Type<StringType>();

        descriptor.Field(x => x.Symbol)
            .Type<StringType>();
    }
}