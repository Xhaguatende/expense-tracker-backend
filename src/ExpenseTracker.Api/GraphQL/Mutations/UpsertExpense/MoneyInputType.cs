// -------------------------------------------------------------------------------------
//  <copyright file="MoneyInputType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Mutations.UpsertExpense;

using Domain.Expenses.ValueObjects;

/// <summary>
/// Represents the input type for the money value object.
/// </summary>
public class MoneyInputType : InputObjectType<Money>
{
    /// <summary>
    /// Configures the money input type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IInputObjectTypeDescriptor<Money> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Value)
            .Type<NonNullType<DecimalType>>();
        descriptor.Field(x => x.Currency)
            .Type<NonNullType<CurrencyInputType>>();
    }
}