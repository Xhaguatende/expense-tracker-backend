// -------------------------------------------------------------------------------------
//  <copyright file="GetCategoriesQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetCategories;

using Domain.Expenses;
using HotChocolate;
using MediatR;

public class GetCategoriesQuery : IRequest<IExecutable<Category>>;