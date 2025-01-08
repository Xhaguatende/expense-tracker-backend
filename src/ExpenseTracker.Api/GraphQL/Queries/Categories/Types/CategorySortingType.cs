// -------------------------------------------------------------------------------------
//  <copyright file="CategorySortingType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Categories.Types;

using ExpenseTracker.Domain.Categories;
using HotChocolate.Data.Sorting;

/// <summary>
/// Defines the category sorting properties to expose.
/// </summary>
public class CategorySortingType : SortInputType<Category>
{
    /// <summary>
    /// Configures the category sorting type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(ISortInputTypeDescriptor<Category> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Name);
    }
}