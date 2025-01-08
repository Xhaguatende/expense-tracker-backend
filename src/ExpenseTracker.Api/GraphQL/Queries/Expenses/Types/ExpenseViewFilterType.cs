// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseViewFilterType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Expenses.Types;

using Domain.Expenses.Views;
using HotChocolate.Data.Filters;

/// <summary>
/// Defines the expense view filter's properties to expose.
/// </summary>
public class ExpenseViewFilterType : FilterInputType<ExpenseView>
{
    /// <summary>
    /// Configures the expense view filter type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IFilterInputTypeDescriptor<ExpenseView> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Title);
        descriptor.Field(x => x.Date);
        descriptor.Field(x => x.CategoryId);
        descriptor.Field(x => x.CurrencySymbol);
        descriptor.Field(x => x.CurrencyIsoSymbol);
    }
}