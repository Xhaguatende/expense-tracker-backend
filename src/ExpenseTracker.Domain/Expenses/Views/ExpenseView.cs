// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseView.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Expenses.Views;

public class ExpenseView
{
    public decimal Amount { get; private set; } = 0;
    public string Category { get; private set; } = string.Empty;
    public Guid CategoryId { get; private set; } = Guid.Empty;
    public string CurrencyIsoSymbol { get; private set; } = string.Empty;
    public string CurrencySymbol { get; private set; } = string.Empty;
    public DateTime Date { get; private set; } = DateTime.MinValue;
    public Guid Id { get; private set; } = Guid.Empty;
    public string Owner { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
}