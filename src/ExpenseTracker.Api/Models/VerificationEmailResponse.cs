// -------------------------------------------------------------------------------------
//  <copyright file="VerificationEmailResponse.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Models;

/// <summary>
/// Represents the verification email response.
/// </summary>
public class VerificationEmailResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the email verification was successful.
    /// </summary>
    public bool Success { get; set; }
}