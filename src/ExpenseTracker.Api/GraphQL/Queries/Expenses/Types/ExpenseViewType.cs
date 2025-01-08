// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseViewType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Expenses.Types;

using ExpenseTracker.Domain.Expenses.Views;

/// <summary>
/// Defines the expense view properties to expose.
/// </summary>
public class ExpenseViewType : ObjectType<ExpenseView>
{
    /// <summary>
    /// Configures the expense view type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IObjectTypeDescriptor<ExpenseView> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.CategoryId);
        descriptor.Field(x => x.Category);
        descriptor.Field(x => x.Title);
        descriptor.Field(x => x.Date);
        descriptor.Field(x => x.Amount);
        descriptor.Field(x => x.CurrencySymbol);
    }
}