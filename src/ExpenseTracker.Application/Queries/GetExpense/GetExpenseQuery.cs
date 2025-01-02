// -------------------------------------------------------------------------------------
//  <copyright file="GetExpenseQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetExpense;

using Domain.Expenses;
using MediatR;

public record GetExpenseQuery(Guid Id) : IRequest<Expense?>;