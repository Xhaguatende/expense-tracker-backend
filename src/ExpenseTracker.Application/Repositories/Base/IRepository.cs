// -------------------------------------------------------------------------------------
//  <copyright file="IRepository.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Application.Repositories.Base;

using System.Linq.Expressions;
using HotChocolate;

public interface IRepository<T>
{
    Task<long> DeleteOneAsync(T document, CancellationToken cancellationToken);

    IExecutable<T> GetManyByExpression(Expression<Func<T, bool>> filter);

    Task<List<T>> GetManyByExpressionAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    Task<T?> GetOneByExpressionAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

    Task<T> UpsertOneAsync(T document, CancellationToken cancellationToken);
}