// -------------------------------------------------------------------------------------
//  <copyright file="GetExpenseQueryHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetExpense;

using Domain.Expenses;
using MediatR;
using Repositories;

public class GetExpenseQueryHandler : IRequestHandler<GetExpenseQuery, Expense?>
{
    private readonly IExpenseRepository _expenseRepository;

    public GetExpenseQueryHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<Expense?> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
    {
        return await _expenseRepository.GetExpenseByIdAsync(request.Id, cancellationToken);
    }
}