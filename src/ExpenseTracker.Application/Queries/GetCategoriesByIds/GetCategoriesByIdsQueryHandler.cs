// -------------------------------------------------------------------------------------
//  <copyright file="GetCategoriesByIdsQueryHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetCategoriesByIds;

using Domain.Expenses;
using MediatR;
using Repositories;

public class GetCategoriesByIdsQueryHandler : IRequestHandler<GetCategoriesByIdsQuery, List<Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesByIdsQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task<List<Category>> Handle(GetCategoriesByIdsQuery request, CancellationToken cancellationToken)
    {
        return _categoryRepository.GetCategoriesByIdsAsync(request.CategoryIds, cancellationToken);
    }
}