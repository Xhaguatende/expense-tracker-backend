// -------------------------------------------------------------------------------------
//  <copyright file="CategoryNotFoundDomainError.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Categories.Errors;

using Shared;

public class CategoryNotFoundDomainError : DomainError
{
    public CategoryNotFoundDomainError(Guid categoryId)
        : base(nameof(CategoryNotFoundDomainError), $"Category with ID '{categoryId}' was not found.")
    {
        CategoryId = categoryId;
    }

    public Guid CategoryId { get; }
}