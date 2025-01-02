// -------------------------------------------------------------------------------------
//  <copyright file="UpsertExpenseCommandValidator.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Commands.UpsertExpense;

using FluentValidation;

public class UpsertExpenseCommandValidator : AbstractValidator<UpsertExpenseCommand>
{
    public UpsertExpenseCommandValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");

        var maxDate = DateTime.UtcNow.AddYears(100);
        var minDate = DateTime.UtcNow.AddYears(-100);

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required")
            .LessThan(maxDate)
            .WithMessage($"Date cannot be after {maxDate}")
            .GreaterThan(minDate)
            .WithMessage($"Date cannot be before {minDate}");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500);

        RuleFor(x => x.Amount)
            .NotNull().WithMessage("Amount is required")
            .Custom((money, context) =>
            {
                if (string.IsNullOrWhiteSpace(money.Currency.IsoSymbol))
                {
                    context.AddFailure("Currency symbol is required");
                }

                if (money.Value <= 0)
                {
                    context.AddFailure("Amount must be greater than 0");
                }
            });
    }
}