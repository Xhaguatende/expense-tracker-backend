// -------------------------------------------------------------------------------------
//  <copyright file="GetCurrenciesQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetCurrencies;

using Domain.Currencies;
using HotChocolate;
using MediatR;

public class GetCurrenciesQuery : IRequest<IExecutable<Currency>>;