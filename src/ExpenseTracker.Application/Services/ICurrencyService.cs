// -------------------------------------------------------------------------------------
//  <copyright file="ICurrencyService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Services;

using Domain.Expenses.ValueObjects;

public interface ICurrencyService
{
    IEnumerable<Currency> GetAllCurrencies();

    Currency? GetCurrency(string isoCurrencySymbol);
}