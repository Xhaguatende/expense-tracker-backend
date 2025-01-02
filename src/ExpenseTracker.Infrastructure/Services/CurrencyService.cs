// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Services;

using System.Globalization;
using Application.Services;
using Domain.Expenses.ValueObjects;

public class CurrencyService : ICurrencyService
{
    private static readonly Dictionary<string, string> _uniqueCurrencies = [];

    public IEnumerable<Currency> GetAllCurrencies()
    {
        var uniqueCurrencies = GetUniqueCurrencies();

        var currencies = uniqueCurrencies.Select(
                currency => new Currency(
                    currency.Key,
                    currency.Value))
            .OrderBy(c => c.IsoSymbol)
            .ToList();

        return currencies;
    }

    public Currency? GetCurrency(string isoCurrencySymbol)
    {
        return GetAllCurrencies()
            .FirstOrDefault(c => c.IsoSymbol.Equals(isoCurrencySymbol, StringComparison.OrdinalIgnoreCase));
    }

    private static Dictionary<string, string> GetUniqueCurrencies()
    {
        if (_uniqueCurrencies.Count > 0)
        {
            return _uniqueCurrencies;
        }

        var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        foreach (var culture in cultures)
        {
            var region = new RegionInfo(culture.Name);

            if (_uniqueCurrencies.ContainsKey(region.ISOCurrencySymbol))
            {
                continue;
            }

            _uniqueCurrencies.Add(region.ISOCurrencySymbol, region.CurrencySymbol);
        }

        return _uniqueCurrencies;
    }
}