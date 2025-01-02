﻿// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Categories.Types;

using ExpenseTracker.Domain.Expenses;

/// <summary>
/// Defines the expense's properties to expose.
/// </summary>
public class CategoryType : ObjectType<Category>
{
    /// <summary>
    /// Configures the expense type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id).Type<IdType>();
        descriptor.Field(x => x.Name);
        descriptor.Field(x => x.Description);
    }
}