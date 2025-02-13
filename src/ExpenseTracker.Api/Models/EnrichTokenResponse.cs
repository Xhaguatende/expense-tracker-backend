﻿// -------------------------------------------------------------------------------------
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
    /// Gets or sets the custom claims.
    /// </summary>
    public Dictionary<string, string> CustomClaims { get; set; } = [];
}