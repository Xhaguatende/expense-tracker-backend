// -------------------------------------------------------------------------------------
//  <copyright file="CurrenciesQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Currencies;

using Application.Queries.GetCurrencies;
using ExpenseTracker.Domain.Currencies;
using HotChocolate.Data;
using MediatR;

/// <summary>
/// The currencies query.
/// </summary>
[QueryType]
public class CurrenciesQuery
{
    /// <summary>
    /// Get the currencies.
    /// </summary>
    /// <param name="mediator">An instance of <see cref="IMediator"/>.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The currencies</returns>
    [GraphQLDescription("Get the currencies.")]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 20)]
    [UseFiltering]
    [UseSorting]
    public async Task<IExecutable<Currency>> GetCurrenciesAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCurrenciesQuery();
        var result = await mediator.Send(query, cancellationToken);

        return result;
    }
}