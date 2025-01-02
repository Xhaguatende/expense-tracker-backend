// -------------------------------------------------------------------------------------
//  <copyright file="UpsertExpenseInput.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Mutations.UpsertExpense;

using Application.Commands.UpsertExpense;

/// <summary>
/// Represents the input for the upsert expense mutation.
/// </summary>
public class UpsertExpenseInput : UpsertExpenseCommand
{
    /// <summary>
    /// Converts the input to an upsert expense command.
    /// </summary>
    /// <returns>The command.</returns>
    public UpsertExpenseCommand ToUpsertExpenseCommand()
    {
        return new UpsertExpenseCommand
        {
            Id = Id,
            CategoryId = CategoryId,
            Title = Title,
            Description = Description,
            Amount = Amount,
            Date = Date
        };
    }
}