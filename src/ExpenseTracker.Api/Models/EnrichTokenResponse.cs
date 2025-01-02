// -------------------------------------------------------------------------------------
//  <copyright file="EnrichTokenResponse.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Models;

/// <summary>
/// Represents the enrich token response.
/// </summary>
public class EnrichTokenResponse
{
    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public List<string> Roles { get; set; } = [];

    /// <summary>
    /// Gets or sets the tenant ID.
    /// </summary>
    public string TenantId { get; set; } = Guid.Empty.ToString();
}