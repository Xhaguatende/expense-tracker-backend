// -------------------------------------------------------------------------------------
//  <copyright file="IApplicationContextService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Services;

public interface IApplicationContextService
{
    string GetClaim(string claimId);

    string GetUserIdentity();
}