// -------------------------------------------------------------------------------------
//  <copyright file="SendEmailVerificationCommandHandler.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Commands.SendEmailVerification;

using MediatR;
using Services;

public class SendEmailVerificationCommandHandler : IRequestHandler<SendEmailVerificationCommand, SendEmailVerificationResponse>
{
    private readonly IAuthService _authService;

    public SendEmailVerificationCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<SendEmailVerificationResponse> Handle(SendEmailVerificationCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.SendEmailVerificationAsync(request.UserId, cancellationToken);

        return new SendEmailVerificationResponse(result);
    }
}