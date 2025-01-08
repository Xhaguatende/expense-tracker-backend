// -------------------------------------------------------------------------------------
//  <copyright file="AuthController.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Controllers;

using Application.Claims;
using Application.Commands.SendEmailVerification;
using MediatR;
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
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{AuthController}"/>.</param>
    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Enriches the token.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The enriched token.
    /// </returns>
    [HttpPost("enrich")]
    [ProducesResponseType<EnrichTokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[Authorize]
    public async Task<IActionResult> EnrichTokenAsync(
        [FromBody] EnrichTokenRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Enriching token for '{UserId}'", request.Email);

        // TODO: Implementation
        await Task.Delay(50, cancellationToken);
        var tenant = request.Email.Contains("@example.com") ? "exampleTenant" : "defaultTenant";
        var roles = new List<string> { "user", "test" };

        var customClaims = new Dictionary<string, string>
        {
            { ExtendedClaimTypes.Email, request.Email },
            { ExtendedClaimTypes.Hostname, request.Hostname},
            { ExtendedClaimTypes.TenantId, tenant },
            { ExtendedClaimTypes.Roles, string.Join(",", roles) }
        };

        var enrichedClaims = new EnrichTokenResponse
        {
            CustomClaims = customClaims
        };

        return Ok(enrichedClaims);
    }

    /// <summary>
    /// Resends the verification email.
    /// </summary>
    /// <param name="mediator">An instance of <see cref="IMediator"/>.</param>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The response from the email verification.
    /// </returns>
    [HttpPost("resend-verification")]
    [ProducesResponseType<VerificationEmailResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> ResendVerificationEmailAsync(
        [Service] IMediator mediator,
        [FromBody] VerificationEmailRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Resend verification link token for '{UserId}'", request.UserId);

        var command = new SendEmailVerificationCommand(request.UserId);

        var response = await mediator.Send(command, cancellationToken);

        return Ok(response);
    }
}