// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseViewRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Repositories;

using Application.Repositories;
using Application.Services;
using Base;
using Domain.Expenses.Views;
using HotChocolate;
using MongoDB.Driver;

public class ExpenseViewRepository : MongoDbViewRepository<ExpenseView>, IExpenseViewRepository
{
    public ExpenseViewRepository(IMongoDatabase mongoDatabase, IApplicationContextService applicationContextService) :
        base(mongoDatabase, applicationContextService)
    {
    }

    protected override string CollectionName => "expensesView";

    public IExecutable<ExpenseView> GetExpensesViewAsync()
    {
        return GetManyByExpression(x => x.Owner == UserIdentity);
    }
}