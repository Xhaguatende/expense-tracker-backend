// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Repositories;

using Application.Repositories;
using Application.Services;
using Base;
using Domain.Expenses;
using HotChocolate;
using MongoDB.Driver;

public class ExpenseRepository : MongoDbRepository<Expense, Guid>, IExpenseRepository
{
    public ExpenseRepository(
        IMongoDatabase mongoDatabase,
        IApplicationContextService applicationContextService) :
        base(mongoDatabase, applicationContextService)
    {
    }

    protected override string CollectionName => "expenses";

    public async Task DeleteAsync(Expense expense, CancellationToken cancellationToken)
    {
        await DeleteOneAsync(expense, cancellationToken);
    }

    public async Task<Expense?> GetExpenseByIdAsync(Guid expenseId, CancellationToken cancellationToken)
    {
        return await GetOneByExpressionAsync(x => x.Id == expenseId && x.Owner == UserIdentity, cancellationToken);
    }

    public IExecutable<Expense> GetExpensesAsync()
    {
        return GetManyByExpression(x => x.Owner == UserIdentity);
    }
}