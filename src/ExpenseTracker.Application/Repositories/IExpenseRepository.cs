// -------------------------------------------------------------------------------------
//  <copyright file="IExpenseRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Repositories;

using Base;
using Domain.Expenses;
using HotChocolate;

public interface IExpenseRepository : IRepository<Expense>
{
    Task<Expense?> GetExpenseByIdAsync(Guid expenseId, CancellationToken cancellationToken);

    IExecutable<Expense> GetExpensesAsync();
}