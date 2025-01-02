// -------------------------------------------------------------------------------------
//  <copyright file="GetCategoriesQueryHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetCategories;

using Domain.Expenses;
using HotChocolate;
using MediatR;
using Repositories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IExecutable<Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task<IExecutable<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_categoryRepository.GetCategoriesAsync());
    }
}