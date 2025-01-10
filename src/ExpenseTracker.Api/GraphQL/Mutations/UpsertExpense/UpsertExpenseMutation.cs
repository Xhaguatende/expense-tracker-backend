// -------------------------------------------------------------------------------------
//  <copyright file="UpsertExpenseMutation.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Mutations.UpsertExpense;

using Domain.Categories.Errors;
using Domain.Currencies.Errors;
using Domain.Expenses;
using Domain.Expenses.Errors;
using Domain.Shared;
using MediatR;
using Types;

/// <summary>
/// Defines the <see cref="UpsertExpenseMutation" />.
/// </summary>
[MutationType]
public class UpsertExpenseMutation : BaseMutation
{
    /// <summary>
    /// Upserts an expense.
    /// </summary>
    /// <param name="mediator">An instance of <see cref="IMediator" />.</param>
    /// <param name="input">The input.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The saved expense.</returns>
    [GraphQLDescription("Upserts an expense.")]
    [UseMutationConvention]
    [Error(typeof(ExpenseNotFoundDomainError))]
    [Error(typeof(CategoryNotFoundDomainError))]
    [Error(typeof(CurrencyNotFoundDomainError))]
    [Error(typeof(DomainError))]
    public async Task<FieldResult<Expense>> UpsertExpenseAsync(
        [Service] IMediator mediator,
        UpsertExpenseInput input,
        CancellationToken cancellationToken = default)
    {
        var command = input.ToUpsertExpenseCommand();

        var result = await mediator.Send(command, cancellationToken);

        return CreateMutationResult(result);
    }
}