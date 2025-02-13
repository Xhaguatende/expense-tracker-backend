﻿// -------------------------------------------------------------------------------------
//  <copyright file="CategoryRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Repositories;

using Application.Repositories;
using Base;
using Domain.Categories;
using ExpenseTracker.Application.Services;
using HotChocolate;
using MongoDB.Driver;

public class CategoryRepository : MongoDbRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(
        IMongoDatabase mongoDatabase,
        IApplicationContextService applicationContextService) :
        base(mongoDatabase, applicationContextService)
    {
    }

    protected override string CollectionName => "categories";

    public IExecutable<Category> GetCategoriesAsync()
    {
        return GetManyByExpression(x => x.Owner == null || x.Owner == UserIdentity);
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await GetOneByExpressionAsync(x => x.Id == categoryId && (x.Owner == null || x.Owner == UserIdentity), cancellationToken);
    }
}