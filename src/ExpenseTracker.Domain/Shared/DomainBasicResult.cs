// -------------------------------------------------------------------------------------
//  <copyright file="DomainBasicResult.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Shared;

public class DomainBasicResult
{
    public DomainBasicResult(string? errorTitle = null, List<DomainError>? errors = null)
    {
        ErrorTitle = errorTitle;
        Errors = errors ?? [];
    }

    public List<DomainError> Errors { get; internal set; }

    public string? ErrorTitle { get; set; }

    public bool IsFailure => !IsSuccess;

    public bool IsSuccess => Errors.Count == 0;
}