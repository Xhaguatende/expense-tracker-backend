// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyHelper.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Helpers;

using System.Globalization;

public class CurrencyHelper
{
    public static string GetCurrencySymbol(string isoCurrencyCode)
    {
        if (string.IsNullOrWhiteSpace(isoCurrencyCode))
        {
            throw new ArgumentException("ISO currency code cannot be null or empty.", nameof(isoCurrencyCode));
        }

        return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(
                culture =>
                {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;  
                    }
                })
            .FirstOrDefault(
                region => region != null && region.ISOCurrencySymbol.Equals(
                    isoCurrencyCode,
                    StringComparison.OrdinalIgnoreCase))?.CurrencySymbol ?? string.Empty;
    }

    public static Dictionary<string, string> GetCurrencySymbols(List<string> isoCurrencyCodes)
    {
        if (isoCurrencyCodes == null || isoCurrencyCodes.Count == 0)
        {
            throw new ArgumentException("ISO currency codes list cannot be null or empty.", nameof(isoCurrencyCodes));
        }

        var specificCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(
                culture =>
                {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
            .Where(region => region != null);

        var currencyLookup = specificCultures.ToDictionary(
            region => region!.ISOCurrencySymbol,
            region => region!.CurrencySymbol,
            StringComparer.OrdinalIgnoreCase);

        return isoCurrencyCodes.ToDictionary(
            code => code,
            code => currencyLookup.TryGetValue(code, out var symbol) ? symbol : string.Empty);
    }
}