// -------------------------------------------------------------------------------------
//  <copyright file="UpsertExpenseCommandHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Commands.UpsertExpense;

using Domain.Expenses;
using Domain.Expenses.Errors;
using Domain.Shared;
using MediatR;
using Repositories;
using Services;

public class UpsertExpenseCommandHandler : IRequestHandler<UpsertExpenseCommand, DomainResult<Expense>>
{
    private readonly IApplicationContextService _applicationContextService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrencyService _currencyService;
    private readonly IExpenseRepository _expenseRepository;

    public UpsertExpenseCommandHandler(
        IExpenseRepository expenseRepository,
        ICategoryRepository categoryRepository,
        IApplicationContextService applicationContextService,
        ICurrencyService currencyService)
    {
        _expenseRepository = expenseRepository;
        _categoryRepository = categoryRepository;
        _applicationContextService = applicationContextService;
        _currencyService = currencyService;
    }

    public async Task<DomainResult<Expense>> Handle(UpsertExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetOneByExpressionAsync(x => x.Id == request.Id, cancellationToken);

        var errors = await ValidateAsync(request, cancellationToken);

        if (errors.Count > 0)
        {
            return DomainResult<Expense>.Failure(errors, "Upsert validation failed.");
        }

        if (expense != null)
        {
            expense.Update(
                request.CategoryId,
                request.Title,
                request.Description,
                request.Amount,
                request.Date);
        }
        else
        {
            var owner = _applicationContextService.GetUserIdentity();

            expense = new Expense(
                request.CategoryId,
                request.Title,
                request.Description,
                request.Amount,
                request.Date,
                owner);
        }

        var savedExpense = await _expenseRepository.UpsertOneAsync(expense, cancellationToken);

        return savedExpense;
    }

    private async Task<List<DomainError>> ValidateAsync(UpsertExpenseCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<DomainError>();

        var category = await _categoryRepository.GetOneByExpressionAsync(
            x => x.Id == request.CategoryId,
            cancellationToken);

        if (category == null)
        {
            errors.Add(new CategoryNotFoundDomainError(request.CategoryId));
        }

        var currency = _currencyService.GetCurrency(request.Amount.Currency.IsoSymbol);

        if (currency == null)
        {
            errors.Add(new CurrencyNotFoundDomainError(request.Amount.Currency.IsoSymbol));
        }
        else
        {
            request.Amount.Currency = currency;
        }

        return errors;
    }
}