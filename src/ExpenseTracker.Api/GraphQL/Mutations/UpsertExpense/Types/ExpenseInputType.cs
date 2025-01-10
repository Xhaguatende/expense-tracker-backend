// -------------------------------------------------------------------------------------
//  <copyright file="ExpenseInputType.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Mutations.UpsertExpense.Types;
/// <summary>
/// Represents the input type for the upsert expense mutation.
/// </summary>
public class ExpenseInputType : InputObjectType<UpsertExpenseInput>
{
    /// <summary>
    /// Configures the expense input type.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void Configure(IInputObjectTypeDescriptor<UpsertExpenseInput> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id)
            .Type<UuidType>();

        descriptor.Field(x => x.CategoryId)
            .Type<NonNullType<UuidType>>();

        descriptor.Field(x => x.Title)
            .Type<NonNullType<StringType>>();

        descriptor.Field(x => x.Description)
            .Type<NonNullType<StringType>>();

        descriptor.Field(x => x.Amount)
            .Type<NonNullType<MoneyInputType>>();

        descriptor.Field(x => x.Date)
            .Type<NonNullType<DateTimeType>>();
    }
}