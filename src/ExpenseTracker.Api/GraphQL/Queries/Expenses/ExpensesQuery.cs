// -------------------------------------------------------------------------------------
//  <copyright file="ExpensesQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQl.Queries.Expenses;

using Application.Queries.GetExpense;
using Application.Queries.GetExpenses;
using Domain.Expenses;
using HotChocolate.Data;
using MediatR;

/// <summary>
/// The expenses query.
/// </summary>
[QueryType]
public class ExpensesQuery
{
    /// <summary>
    /// Gets an expense.
    /// </summary>
    /// <param name="mediator">An instance of <see cref="IMediator" />.</param>
    /// <param name="id">The expense ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The expense.</returns>
    [GraphQLDescription("Gets an expense.")]
    public async Task<Expense?> GetExpenseAsync(
        [Service] IMediator mediator,
        [ID] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetExpenseQuery(id);
        var result = await mediator.Send(query, cancellationToken);

        return result;
    }

    /// <summary>
    /// Get the expenses.
    /// </summary>
    /// <param name="mediator">An instance of <see cref="IMediator" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The expense.</returns>
    [GraphQLDescription("Get the expenses.")]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 20)]
    [UseFiltering]
    [UseSorting]
    public async Task<IExecutable<Expense>> GetExpensesAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var query = new GetExpensesQuery();
        var result = await mediator.Send(query, cancellationToken);

        return result;
    }
}