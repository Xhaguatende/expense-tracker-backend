// -------------------------------------------------------------------------------------
//  <copyright file="IExpenseRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Repositories;

using Base;
using Domain.Expenses;
using HotChocolate;

public interface ICategoryRepository : IRepository<Category>
{
    IExecutable<Category> GetCategoriesAsync();

    Task<List<Category>> GetCategoriesByIdsAsync(List<Guid> categoryIds, CancellationToken cancellationToken);

    Task<Category?> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken);
}