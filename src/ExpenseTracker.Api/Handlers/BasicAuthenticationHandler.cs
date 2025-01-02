// -------------------------------------------------------------------------------------
//  <copyright file="BasicAuthenticationHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.Handlers;

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

/// <summary>
/// Handles the basic authentication.
/// </summary>
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    /// <summary>
    ///     The logger.
    /// </summary>
    private readonly ILogger<BasicAuthenticationHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicAuthenticationHandler"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="logger">The logger factory.</param>
    /// <param name="encoder">The URL encoder.</param>
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
        _logger = logger.CreateLogger<BasicAuthenticationHandler>();
    }

    /// <summary>
    /// Handles the request and validates the basic authentication (username/password).
    /// </summary>
    /// <returns>The authentication result.</returns>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authorization))
        {
            Response.Headers.Append("WWW-Authenticate", "Basic realm=\"basic\"");
            const string warningMessage = "Authorization header missing.";
            _logger.LogWarning(warningMessage);

            return Task.FromResult(AuthenticateResult.Fail(warningMessage));
        }

        if (string.IsNullOrWhiteSpace(authorization) || !authorization.ToString().StartsWith("Basic "))
        {
            return Task.FromResult(AuthenticateResult.Fail("Not Basic Auth!"));
        }

        string username;
        string password;

        try
        {
            var authenticationHeader = AuthenticationHeaderValue.Parse(authorization!);
            var credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter!);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split([':'], 2);
            username = credentials[0];
            password = credentials[1];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving the credentials from the authentication header.");
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header."));
        }

        if (username == "" || password == "")
        {
            _logger.LogWarning("Invalid credentials for username: '{@Username}'", username);

            return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password."));
        }

        var claims = new[]
        {
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username),
            new Claim(ClaimTypes.Email, $"{username}@tracker-expenses.com")
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}