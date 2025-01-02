// -------------------------------------------------------------------------------------
//  <copyright file="Auth0Settings.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Settings;

/// <summary>
/// Represents settings for Auth0.
/// </summary>
public class Auth0Settings
{
    /// <summary>
    /// Gets or sets the audience.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the domain.
    /// </summary>
    public string Domain { get; set; } = string.Empty;
}