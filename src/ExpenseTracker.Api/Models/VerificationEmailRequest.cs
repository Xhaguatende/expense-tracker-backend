// -------------------------------------------------------------------------------------
//  <copyright file="VerificationEmailRequest.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Models;

/// <summary>
/// Represents the verification email request.
/// </summary>
public class VerificationEmailRequest
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    public required string  UserId { get; set; }
}