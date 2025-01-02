// -------------------------------------------------------------------------------------
//  <copyright file="HttpApplicationContextService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Services;

using Application.Claims;
using Application.Services;
using Microsoft.AspNetCore.Http;

public class HttpApplicationContextService : IApplicationContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpApplicationContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetClaim(string claimId)
    {
        var user = GetHttpContext().User;
        var claim = user.Claims.FirstOrDefault(claim => claim.Type == claimId);
        return claim == null ? string.Empty : claim.Value;
    }

    public string GetUserIdentity()
    {
        var user = GetHttpContext().User;

        var claim = GetClaim(ExtendedClaimTypes.Email);

        return (string.IsNullOrWhiteSpace(claim) ? user.Identity?.Name : claim)!;
    }

    private HttpContext GetHttpContext()
    {
        return _httpContextAccessor.HttpContext!;
    }
}