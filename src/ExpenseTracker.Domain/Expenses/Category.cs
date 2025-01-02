// -------------------------------------------------------------------------------------
//  <copyright file="Category.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Expenses;

using Primitives;

public class Category : Entity<Guid>
{
    public Category(string name, string description, string? owner) : base(Guid.CreateVersion7())
    {
        Name = name;
        Description = description;
        Owner = owner;
    }

    public string? Description { get; private set; }
    public string Name { get; private set; }
    public string? Owner { get; private set; }
}