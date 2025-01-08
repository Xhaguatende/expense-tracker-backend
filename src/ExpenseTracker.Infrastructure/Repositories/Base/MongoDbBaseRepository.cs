// -------------------------------------------------------------------------------------
//  <copyright file="MongoDbBaseRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Repositories.Base;

using Application.Services;
using MongoDB.Driver;

public abstract class MongoDbBaseRepository<T>
{
    protected readonly IApplicationContextService ApplicationContextService;

    protected readonly string UserIdentity;

    protected MongoDbBaseRepository(IMongoDatabase mongoDatabase, IApplicationContextService applicationContextService)
    {
        ApplicationContextService = applicationContextService;
        UserIdentity = applicationContextService.GetUserIdentity();

        // ReSharper disable once VirtualMemberCallInConstructor
        Collection = mongoDatabase.GetCollection<T>(CollectionName);
    }

    protected IMongoCollection<T> Collection { get; set; }

    protected abstract string CollectionName { get; }
}