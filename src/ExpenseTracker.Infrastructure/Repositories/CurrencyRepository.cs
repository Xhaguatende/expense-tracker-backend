// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Repositories;

using Application.Services;
using Base;
using Domain.Currencies;
using Domain.Currencies.ValueObjects;
using ExpenseTracker.Application.Repositories;
using HotChocolate;
using MongoDB.Driver;

public class CurrencyRepository : MongoDbRepository<Currency, CurrencyId>, ICurrencyRepository
{
    public CurrencyRepository(IMongoDatabase mongoDatabase, IApplicationContextService applicationContextService) :
        base(mongoDatabase, applicationContextService)
    {
    }

    protected override string CollectionName => "currencies";

    public IExecutable<Currency> GetCurrencies()
    {
        return GetManyByExpression(x => true);
    }

    public async Task<Currency?> GetCurrencyAsync(string isoSymbol, CancellationToken cancellationToken = default)
    {
        return await GetOneByExpressionAsync(x => x.IsoSymbol == isoSymbol, cancellationToken);
    }
}