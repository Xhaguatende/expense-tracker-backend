// -------------------------------------------------------------------------------------
//  <copyright file="DomainError.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Shared;

using System.Text;

public class DomainError
{
    public DomainError(
        string code = "",
        string message = "",
        string property = "")
    {
        Code = code;
        Message = message;
        Property = property;
    }

    public string? Code { get; }

    public string? Message { get; }

    public string? Property { get; set; }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append($"Code: '{Code}' | ");
        builder.Append($"Message: '{Message}'");
        builder.Append($"Property: '{Property}' | ");

        return builder.ToString();
    }
}