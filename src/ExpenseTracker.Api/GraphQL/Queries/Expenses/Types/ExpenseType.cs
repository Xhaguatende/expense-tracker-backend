﻿// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Expenses.Types;

using Domain.Expenses;

/// <summary>
/// Defines the expense's properties to expose.
/// </summary>
public class ExpenseType : ObjectType<Expense>
{
    /// <summary>
    /// Configures the expense type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IObjectTypeDescriptor<Expense> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Amount).Type<MoneyType>();
        descriptor.Field(x => x.Title);
        descriptor.Field(x => x.Description);
        descriptor.Field(x => x.Date);
        descriptor.Field(x => x.CategoryId);
    }
}