// -------------------------------------------------------------------------------------
//  <copyright file="AuthController.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

/// <summary>
/// Represents the authentication controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Enriches the token.
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns>
    /// The enriched token.
    /// </returns>
    [HttpPost("enrich")]
    [ProducesResponseType<EnrichTokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult EnrichToken([FromBody] EnrichTokenRequest request)
    {
        // TODO: Implementation
        var tenant = request.Email.Contains("@example.com") ? "exampleTenant" : "defaultTenant";

        var enrichedClaims = new EnrichTokenResponse
        {
            TenantId = tenant,
            Roles = ["user"]
        };

        return Ok(enrichedClaims);
    }
}