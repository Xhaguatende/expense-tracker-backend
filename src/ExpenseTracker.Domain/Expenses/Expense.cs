// -------------------------------------------------------------------------------------
//  <copyright file="Expense.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Expenses;

using Primitives;
using ValueObjects;

public class Expense : Entity<Guid>
{
    public Expense(
        Guid categoryId,
        string title,
        string description,
        Money amount,
        DateTime date,
        string owner)
        : base(Guid.CreateVersion7())
    {
        CategoryId = categoryId;
        Title = title;
        Description = description;
        Amount = amount;
        Date = date;
        Owner = owner;
    }

    public Money Amount { get; private set; }
    public Guid CategoryId { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public string Owner { get; private set; }
    public string Title { get; private set; }

    public void Update(
        Guid categoryId,
        string title,
        string description,
        Money amount,
        DateTime date)
    {
        CategoryId = categoryId;
        Title = title;
        Description = description;
        Date = date;
        Amount = amount;
    }
}