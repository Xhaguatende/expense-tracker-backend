// -------------------------------------------------------------------------------------
//  <copyright file="BaseMutation.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQL.Mutations;

using Domain.Shared;

/// <summary>
/// Represents the base mutation.
/// </summary>
public abstract class BaseMutation
{
    /// <summary>
    /// Creates a mutation result.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="result">The result</param>
    /// <returns>The <see cref="FieldResult{T}"/></returns>
    protected static FieldResult<T> CreateMutationResult<T>(DomainResult<T> result)
    {
        return result.IsFailure ? new FieldResult<T>(result.Errors) : result.Value!;
    }

    /// <summary>
    /// Creates a mutation result.
    /// </summary>
    /// <param name="result">The result</param>
    /// <returns>The <see cref="FieldResult{Boolean}"/></returns>
    protected static FieldResult<bool> CreateMutationResult(DomainBasicResult result)
    {
        return result.IsFailure ? new FieldResult<bool>(result.Errors) : result.IsSuccess;
    }
}