// -------------------------------------------------------------------------------------
//  <copyright file="UpsertExpenseCommand.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Commands.UpsertExpense;

using Domain.Expenses;
using Domain.Expenses.ValueObjects;
using Domain.Shared;
using MediatR;

public class UpsertExpenseCommand : IRequest<DomainResult<Expense>>
{
    public required Money Amount { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid? Id { get; set; }
    public string Title { get; set; } = string.Empty;
}