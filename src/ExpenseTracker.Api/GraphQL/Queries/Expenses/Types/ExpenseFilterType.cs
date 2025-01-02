// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseFilterType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Expenses.Types;

using Domain.Expenses;
using HotChocolate.Data.Filters;

/// <summary>
/// Defines the expense filter's properties to expose.
/// </summary>
public class CategoryFilterType : FilterInputType<Expense>
{
    /// <summary>
    /// Configures the expense filter type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IFilterInputTypeDescriptor<Expense> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Title);
        descriptor.Field(x => x.Date);
        descriptor.Field(x => x.CategoryId);
    }
}