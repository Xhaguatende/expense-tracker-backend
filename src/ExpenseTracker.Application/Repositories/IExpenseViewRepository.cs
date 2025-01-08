// -------------------------------------------------------------------------------------
//  <copyright file="IExpenseViewRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Repositories;

using Domain.Expenses.Views;
using HotChocolate;

public interface IExpenseViewRepository
{
    IExecutable<ExpenseView> GetExpensesViewAsync();
}