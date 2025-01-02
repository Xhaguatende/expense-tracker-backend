// -------------------------------------------------------------------------------------
//  <copyright file="CategoryBatchDataLoader.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.DataLoaders;

using Application.Queries.GetCategoriesByIds;
using Domain.Expenses;
using MediatR;

/// <summary>
/// Represents the category batch data loader.
/// </summary>
public class CategoryBatchDataLoader : BatchDataLoader<Guid, Category>
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryBatchDataLoader"/> class.
    /// </summary>
    /// <param name="batchScheduler">An instance of <see cref="IBatchScheduler"/>.</param>
    /// <param name="options">The data loader options.</param>
    /// <param name="mediator">An instance of <see cref="IMediator"/>.</param>
    public CategoryBatchDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        IMediator mediator) : base(
        batchScheduler,
        options)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Loads the categories based on the IDs.
    /// </summary>
    /// <param name="keys">The list of category IDs.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection with the categories.</returns>
    protected override async Task<IReadOnlyDictionary<Guid, Category>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoriesByIdsQuery(keys.ToList());

        var categories = await _mediator.Send(query, cancellationToken);

        return categories.ToDictionary(x => x.Id);
    }
}