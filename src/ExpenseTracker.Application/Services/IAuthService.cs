// -------------------------------------------------------------------------------------
//  <copyright file="IAuthService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Services;

public interface IAuthService
{
    Task<bool> SendEmailVerificationAsync(string userId, CancellationToken cancellationToken);
}