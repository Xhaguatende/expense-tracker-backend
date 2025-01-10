// -------------------------------------------------------------------------------------
//  <copyright file="DeleteExpenseMutation.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Mutations.DeleteExpense;

using Domain.Expenses.Errors;
using MediatR;
using Types;

/// <summary>
/// Defines the <see cref="DeleteExpenseMutation" />.
/// </summary>
[MutationType]
public class DeleteExpenseMutation : BaseMutation
{
    /// <summary>
    /// Deletes an expense.
    /// </summary>
    /// <param name="mediator">An instance of <see cref="IMediator"/>.</param>
    /// <param name="input">The input.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [GraphQLDescription("Deletes an expense.")]
    [UseMutationConvention]
    [Error(typeof(ExpenseNotFoundDomainError))]
    public async Task<FieldResult<bool>> DeleteExpenseAsync(
        [Service] IMediator mediator,
        DeleteExpenseInput input,
        CancellationToken cancellationToken = default)
    {
        var command = input.ToDeleteExpenseCommand();
        var result = await mediator.Send(command, cancellationToken);

        return CreateMutationResult(result);
    }
}