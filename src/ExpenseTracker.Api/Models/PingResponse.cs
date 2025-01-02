// -------------------------------------------------------------------------------------
//  <copyright file="PingResponse.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Models;

/// <summary>
/// Represents a response to a ping request.
/// </summary>
/// <param name="Message">The message.</param>
/// <param name="DateTime">The current date/time (UTC).</param>
public record PingResponse(string Message, DateTime DateTime);