// -------------------------------------------------------------------------------------
//  <copyright file="DeleteExpenseCommand.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Commands.DeleteExpense;

using Domain.Shared;
using MediatR;

public record DeleteExpenseCommand(Guid Id) : IRequest<DomainBasicResult>;