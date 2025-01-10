// -------------------------------------------------------------------------------------
//  <copyright file="DeleteExpenseCommandHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Commands.DeleteExpense;

using Domain.Expenses.Errors;
using Domain.Shared;
using MediatR;
using Repositories;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, DomainBasicResult>
{
    private readonly IExpenseRepository _expenseRepository;

    public DeleteExpenseCommandHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<DomainBasicResult> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetExpenseByIdAsync(request.Id, cancellationToken);

        if (expense == null)
        {
            return new DomainBasicResult(errors: [new ExpenseNotFoundDomainError(request.Id)]);
        }

        await _expenseRepository.DeleteAsync(expense, cancellationToken);
        
        return new DomainBasicResult();
    }
}