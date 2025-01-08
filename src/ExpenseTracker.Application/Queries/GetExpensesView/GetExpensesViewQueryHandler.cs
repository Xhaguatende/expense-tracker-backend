// -------------------------------------------------------------------------------------
//  <copyright file="GetExpensesViewQueryHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetExpensesView;

using Domain.Expenses.Views;
using HotChocolate;
using MediatR;
using Repositories;

public class GetExpensesViewQueryHandler : IRequestHandler<GetExpensesViewQuery, IExecutable<ExpenseView>>
{
    private readonly IExpenseViewRepository _expenseRepository;

    public GetExpensesViewQueryHandler(IExpenseViewRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public Task<IExecutable<ExpenseView>> Handle(GetExpensesViewQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_expenseRepository.GetExpensesViewAsync());
    }
}