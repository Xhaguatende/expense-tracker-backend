﻿// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseFilterType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Categories.Types;

using ExpenseTracker.Domain.Expenses;
using HotChocolate.Data.Filters;

/// <summary>
/// Defines the expense filter's properties to expose.
/// </summary>
public class CategoryFilterType : FilterInputType<Category>
{
    /// <summary>
    /// Configures the expense filter type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IFilterInputTypeDescriptor<Category> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Name);
    }
}