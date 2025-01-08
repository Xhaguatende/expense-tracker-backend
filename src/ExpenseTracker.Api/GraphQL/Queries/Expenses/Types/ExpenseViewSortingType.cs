// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseViewSortingType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Expenses.Types;

using Domain.Expenses.Views;
using HotChocolate.Data.Sorting;

/// <summary>
/// Defines the expense view sorting properties to expose.
/// </summary>
public class ExpenseViewSortingType : SortInputType<ExpenseView>
{
    /// <summary>
    /// Configures the expense view sorting type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(ISortInputTypeDescriptor<ExpenseView> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Title);
        descriptor.Field(x => x.Category);
        descriptor.Field(x => x.Amount);
        descriptor.Field(x => x.Date);
    }
}