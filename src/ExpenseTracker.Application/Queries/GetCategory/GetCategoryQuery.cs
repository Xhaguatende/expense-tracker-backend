﻿// -------------------------------------------------------------------------------------
//  <copyright file="GetCategoryQuery.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Queries.GetCategory;

using Domain.Categories;
using MediatR;

public record GetCategoryQuery(Guid Id) : IRequest<Category?>;