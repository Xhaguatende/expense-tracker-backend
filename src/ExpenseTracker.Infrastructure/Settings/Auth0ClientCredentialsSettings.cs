// -------------------------------------------------------------------------------------
//  <copyright file="Auth0ClientCredentialsSettings.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Settings;

public class Auth0ClientCredentialsSettings
{
    /// <summary>
    /// Gets or sets the audience.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Gets the base URL.
    /// </summary>
    public string BaseUrl => $"https://{Domain}";

    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the client secret.
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the domain.
    /// </summary>
    public string Domain { get; set; } = string.Empty;
}