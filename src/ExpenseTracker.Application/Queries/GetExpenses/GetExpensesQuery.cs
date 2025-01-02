// -------------------------------------------------------------------------------------
//  <copyright file="GetExpensesQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetExpenses;

using Domain.Expenses;
using HotChocolate;
using MediatR;

public record GetExpensesQuery : IRequest<IExecutable<Expense>>;