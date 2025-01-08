// -------------------------------------------------------------------------------------
//  <copyright file="GetExpensesViewQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetExpensesView;

using Domain.Expenses.Views;
using HotChocolate;
using MediatR;

public record GetExpensesViewQuery : IRequest<IExecutable<ExpenseView>>;