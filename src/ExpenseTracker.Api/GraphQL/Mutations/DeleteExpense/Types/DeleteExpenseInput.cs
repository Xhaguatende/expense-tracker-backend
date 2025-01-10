// -------------------------------------------------------------------------------------
//  <copyright file="DeleteExpenseInput.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Mutations.DeleteExpense.Types;

using ExpenseTracker.Application.Commands.DeleteExpense;

/// <summary>
/// Represents the input for the delete expense mutation.
/// </summary>
public record DeleteExpenseInput : DeleteExpenseCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteExpenseInput"/> class.
    /// </summary>
    /// <param name="id">The expense ID</param>
    public DeleteExpenseInput(Guid id) : base(id)
    {
    }

    /// <summary>
    /// Converts the input to a delete expense command.
    /// </summary>
    /// <returns></returns>
    public DeleteExpenseCommand ToDeleteExpenseCommand()
    {
        return new DeleteExpenseCommand(Id);
    }
}