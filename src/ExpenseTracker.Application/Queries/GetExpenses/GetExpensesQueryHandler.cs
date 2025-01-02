// -------------------------------------------------------------------------------------
//  <copyright file="GetExpensesQueryHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetExpenses;

using Domain.Expenses;
using HotChocolate;
using MediatR;
using Repositories;

public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, IExecutable<Expense>>
{
    private readonly IExpenseRepository _expenseRepository;

    public GetExpensesQueryHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public Task<IExecutable<Expense>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_expenseRepository.GetExpensesAsync());
    }
}