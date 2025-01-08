// -------------------------------------------------------------------------------------
//  <copyright file="ICurrencyRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Repositories;

using Domain.Currencies;
using HotChocolate;

public interface ICurrencyRepository
{
    IExecutable<Currency> GetCurrencies();

    Task<Currency?> GetCurrencyAsync(string isoSymbol, CancellationToken cancellationToken = default);
}