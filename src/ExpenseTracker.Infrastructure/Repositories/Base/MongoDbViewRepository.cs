// -------------------------------------------------------------------------------------
//  <copyright file="MongoDbViewRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Repositories.Base;

using System.Linq.Expressions;
using Application.Services;
using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;

public abstract class MongoDbViewRepository<T> : MongoDbBaseRepository<T> where T : class
{
    protected MongoDbViewRepository(
        IMongoDatabase mongoDatabase,
        IApplicationContextService applicationContextService)
        : base(mongoDatabase, applicationContextService)
    {
    }

    public IExecutable<T> GetManyByExpression(Expression<Func<T, bool>> filter)
    {
        var filterDefinition = BuildFilterDefinition(filter);

        return Collection.Find(filterDefinition).AsExecutable();
    }

    private static FilterDefinition<T> BuildFilterDefinition(Expression<Func<T, bool>>? filter)
    {
        var filterDefinition = filter == null
            ? Builders<T>.Filter.Empty
            : Builders<T>.Filter.Where(filter);

        return filterDefinition;
    }
}