﻿// -------------------------------------------------------------------------------------
//  <copyright file="IExpenseRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Repositories;

using Base;
using Domain.Categories;
using HotChocolate;

public interface ICategoryRepository : IRepository<Category>
{
    IExecutable<Category> GetCategoriesAsync();

    Task<Category?> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken);
}