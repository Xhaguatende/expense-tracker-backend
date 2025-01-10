// -------------------------------------------------------------------------------------
//  <copyright file="CurrencyType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Queries.Currencies.Types;

using Application.Helpers;
using Domain.Currencies;

/// <summary>
/// Defines the currency's properties to expose.
/// </summary>
public class CurrencyType : ObjectType<Currency>
{
    /// <summary>
    /// Configures the currency type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IObjectTypeDescriptor<Currency> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.IsoSymbol);
        descriptor.Field(x => x.Name);

        descriptor.Field("symbol")
            .Type<StringType>()
            .Resolve(
                context =>
                {
                    var currency = context.Parent<Currency>();

                    return CurrencyHelper.GetCurrencySymbol(currency.IsoSymbol);
                });
    }
}