// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseNotFoundDomainError.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Expenses.Errors;

using Shared;

public class ExpenseNotFoundDomainError : DomainError
{
    public ExpenseNotFoundDomainError(Guid id)
        : base(nameof(ExpenseNotFoundDomainError), $"Expense with ID '{id}' was not found.")
    {
        Id = id;
    }

    public Guid Id { get; }
}