// -------------------------------------------------------------------------------------
//  <copyright file="GetCategoryQueryHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetCategory;

using Domain.Expenses;
using MediatR;
using Repositories;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Category?>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category?> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetCategoryByIdAsync(request.Id, cancellationToken);
    }
}