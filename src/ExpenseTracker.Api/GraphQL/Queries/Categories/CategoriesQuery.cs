// -------------------------------------------------------------------------------------
//  <copyright file="CategoriesQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Categories;

using ExpenseTracker.Application.Queries.GetCategories;
using ExpenseTracker.Application.Queries.GetCategory;
using ExpenseTracker.Domain.Categories;
using HotChocolate.Data;
using MediatR;

/// <summary>
/// The categories query.
/// </summary>
[QueryType]
public class CategoriesQuery
{
    /// <summary>
    /// Get the categories.
    /// </summary>
    /// <param name="mediator">An instance of <see cref="IMediator" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The Category.</returns>
    [GraphQLDescription("Get the Categories.")]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 20)]
    [UseFiltering]
    [UseSorting]
    public async Task<IExecutable<Category>> GetCategoriesAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCategoriesQuery();
        var result = await mediator.Send(query, cancellationToken);

        return result;
    }

    /// <summary>
    /// Gets a category.
    /// </summary>
    /// <param name="mediator">An instance of <see cref="IMediator" />.</param>
    /// <param name="id">The Category ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The category.</returns>
    [GraphQLDescription("Gets a category.")]
    public async Task<Category?> GetCategoryAsync(
        [Service] IMediator mediator,
        [ID] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCategoryQuery(id);
        var result = await mediator.Send(query, cancellationToken);

        return result;
    }
}