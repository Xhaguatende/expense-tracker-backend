// -------------------------------------------------------------------------------------
//  <copyright file="MongoDbRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Repositories.Base;

using System.Linq.Expressions;
using Application.Services;
using Domain.Primitives;
using ExpenseTracker.Application.Repositories.Base;
using Extensions;
using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;

public abstract class MongoDbRepository<T, TKey> : MongoDbBaseRepository<T>, IRepository<T> where T : Entity<TKey> where TKey : notnull
{
    protected MongoDbRepository(
        IMongoDatabase mongoDatabase,
        IApplicationContextService applicationContextService)
        : base(mongoDatabase, applicationContextService)
    {
    }

    public async Task<long> DeleteOneAsync(T document, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(t => t.Id, document.Id);
        var result = await Collection.DeleteOneAsync(filter, cancellationToken);

        return result.DeletedCount;
    }

    public IExecutable<T> GetManyByExpression(Expression<Func<T, bool>> filter)
    {
        var filterDefinition = BuildFilterDefinition(filter);

        return Collection.Find(filterDefinition).AsExecutable();
    }

    public async Task<List<T>> GetManyByExpressionAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        var filterDefinition = BuildFilterDefinition(filter);

        return await Collection.Find(filterDefinition).ToListAsync(cancellationToken);
    }

    public async Task<T?> GetOneByExpressionAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        var filterDefinition = BuildFilterDefinition(filter);

        return await Collection.Find(filterDefinition).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T> UpsertOneAsync(T document, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(t => t.Id, document.Id);

        var excludeProperties = new[]
        {
            nameof(document.CreatedAt),
            nameof(document.CreatedBy),
            nameof(document.UpdatedAt),
            nameof(document.UpdatedBy),
            nameof(document.Version)
        };

        var update = Builders<T>.Update
            .SetAll(document, excludeProperties)
            .SetOnInsert(t => t.CreatedAt, DateTime.UtcNow)
            .SetOnInsert(t => t.CreatedBy, UserIdentity)
            .Set(t => t.UpdatedAt, DateTime.UtcNow)
            .Set(t => t.UpdatedBy, UserIdentity)
            .Inc(t => t.Version, 1);

        var updateOptions = new UpdateOptions { IsUpsert = true };

        await Collection.UpdateOneAsync(filter, update, updateOptions, cancellationToken);

        return document;
    }

    private static FilterDefinition<T> BuildFilterDefinition(Expression<Func<T, bool>>? filter)
    {
        var filterDefinition = filter == null
            ? Builders<T>.Filter.Empty
            : Builders<T>.Filter.Where(filter);

        return filterDefinition;
    }
}