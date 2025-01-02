// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Expenses.Types;

using DataLoaders;
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
        descriptor.Field(x => x.Amount);
        descriptor.Field(x => x.Title);
        descriptor.Field(x => x.Description);
        descriptor.Field(x => x.Date);
        descriptor.Field(x => x.CategoryId);

        descriptor.Field("category")
            .Type<StringType>()
            .Resolve(
                async (context, ct) =>
                {
                    var expense = context.Parent<Expense>();

                    var category = await context.DataLoader<CategoryBatchDataLoader>()
                        .LoadAsync(expense.CategoryId, ct);

                    return category == null ? string.Empty : category.Name;
                });
    }
}