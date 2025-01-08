// -------------------------------------------------------------------------------------
//  <copyright file="ExtendedClaimTypes.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Claims;

public static class ExtendedClaimTypes
{
    public const string Email = $"{Prefix}/email";
    public const string Roles = $"{Prefix}/roles";
    public const string TenantId = $"{Prefix}/tenant_id";
    public const string Hostname = $"{Prefix}/hostname";


    private const string Prefix = "https://tracker-expense.com";
}