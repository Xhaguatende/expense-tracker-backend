// -------------------------------------------------------------------------------------
//  <copyright file="AuthService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using ExpenseTracker.Application.Services;
using Microsoft.Extensions.Options;
using Settings;

public class AuthService : IAuthService
{
    private readonly Auth0ClientCredentialsSettings _auth0ClientCredentialsSettings;
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient, IOptions<Auth0ClientCredentialsSettings> auth0ClientCredentialsSettings)
    {
        _httpClient = httpClient;
        _auth0ClientCredentialsSettings = auth0ClientCredentialsSettings.Value;
    }

    public async Task<bool> SendEmailVerificationAsync(string userId, CancellationToken cancellationToken)
    {
        var accessToken = await GetAccessToken(CancellationToken.None);

        var payload = new
        {
            user_id = userId
        };

        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.PostAsJsonAsync("api/v2/jobs/verification-email", payload, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    private async Task<string> GetAccessToken(CancellationToken cancellationToken)
    {
        var audience = _auth0ClientCredentialsSettings.Audience;
        var clientId = _auth0ClientCredentialsSettings.ClientId;
        var clientSecret = _auth0ClientCredentialsSettings.ClientSecret;

        var tokenPayload = new
        {
            client_id = clientId,
            client_secret = clientSecret,
            audience,
            grant_type = "client_credentials",
            scope = "read:users update:users"
        };

        var response = await _httpClient.PostAsJsonAsync("oauth/token", tokenPayload, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Failed to retrieve Auth0 token.");
        }

        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        var responseJson = JsonSerializer.Deserialize<JsonElement>(responseBody);

        var accessToken = responseJson.GetProperty("access_token").GetString();

        return accessToken ?? string.Empty;
    }
}