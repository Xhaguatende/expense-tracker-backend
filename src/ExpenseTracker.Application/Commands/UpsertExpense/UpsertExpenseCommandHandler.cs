// -------------------------------------------------------------------------------------
//  <copyright file="UpsertExpenseCommandHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Commands.UpsertExpense;

using Domain.Categories.Errors;
using Domain.Currencies.Errors;
using Domain.Expenses;
using Domain.Shared;
using MediatR;
using Repositories;
using Services;

public class UpsertExpenseCommandHandler : IRequestHandler<UpsertExpenseCommand, DomainResult<Expense>>
{
    private readonly IApplicationContextService _applicationContextService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IExpenseRepository _expenseRepository;

    public UpsertExpenseCommandHandler(
        IExpenseRepository expenseRepository,
        ICategoryRepository categoryRepository,
        IApplicationContextService applicationContextService,
        ICurrencyRepository currencyRepository)
    {
        _expenseRepository = expenseRepository;
        _categoryRepository = categoryRepository;
        _applicationContextService = applicationContextService;
        _currencyRepository = currencyRepository;
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

        var currency = await _currencyRepository.GetCurrencyAsync(request.Amount.CurrencyIsoSymbol, cancellationToken);

        if (currency == null)
        {
            errors.Add(new CurrencyNotFoundDomainError(request.Amount.CurrencyIsoSymbol));
        }

        return errors;
    }
}