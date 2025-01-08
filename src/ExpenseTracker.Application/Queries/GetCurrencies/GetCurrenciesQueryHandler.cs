// -------------------------------------------------------------------------------------
//  <copyright file="GetCurrenciesQueryHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetCurrencies;

using Domain.Currencies;
using HotChocolate;
using MediatR;
using Repositories;

public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, IExecutable<Currency>>
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetCurrenciesQueryHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public Task<IExecutable<Currency>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_currencyRepository.GetCurrencies());
    }
}