// -------------------------------------------------------------------------------------
//  <copyright file="HotChocolateErrorFilter.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Api.GraphQl.Filters;

using System.Diagnostics;

/// <summary>
/// Handles GraphQL errors.
/// </summary>
public class HotChocolateErrorFilter : IErrorFilter
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<HotChocolateErrorFilter> _logger;

    /// <summary>
    /// Initialises a new instance of the <see cref="HotChocolateErrorFilter"/> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{HotChocolateErrorFilter}"/></param>
    public HotChocolateErrorFilter(ILogger<HotChocolateErrorFilter> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Writes the exception to the log.
    /// </summary>
    /// <param name="error">The error that occurred.</param>
    /// <returns>Returns the error passed in to this filter or a rewritten error.</returns>
    public IError OnError(IError error)
    {
        if (error.Exception == null)
        {
            return error;
        }

        Activity.Current?.AddException(error.Exception);
        Activity.Current?.SetStatus(ActivityStatusCode.Error, error.Message);
        Activity.Current?.SetTag("graphql.error.path", error.Path);

        _logger.LogError(error.Exception, "Unhandled exception on GraphQL execution.");

        return error;
    }
}