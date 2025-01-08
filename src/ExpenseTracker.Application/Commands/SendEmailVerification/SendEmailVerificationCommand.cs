// -------------------------------------------------------------------------------------
//  <copyright file="SendEmailVerificationCommand.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Commands.SendEmailVerification;

using MediatR;

public record SendEmailVerificationCommand(string UserId) : IRequest<SendEmailVerificationResponse>;