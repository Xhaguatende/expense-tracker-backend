// -------------------------------------------------------------------------------------
//  <copyright file="GetCategoriesByIdsQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetCategoriesByIds;

using Domain.Expenses;
using MediatR;

public record GetCategoriesByIdsQuery(List<Guid> CategoryIds) : IRequest<List<Category>>;