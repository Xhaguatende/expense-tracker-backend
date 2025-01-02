// -------------------------------------------------------------------------------------
//  <copyright file="EnrichTokenRequest.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Models;

/// <summary>
/// Represents the enrich token request.
/// </summary>
public class EnrichTokenRequest
{
    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hostname.
    /// </summary>
    public string Hostname { get; set; } = string.Empty;
}