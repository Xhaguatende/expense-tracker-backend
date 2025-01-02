// -------------------------------------------------------------------------------------
//  <copyright file="MongoExtensions.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Extensions;

using System.Reflection;
using MongoDB.Driver;

public static class MongoExtensions
{
    public static UpdateDefinition<TDocument> SetAll<TDocument, TModel>(
        this UpdateDefinitionBuilder<TDocument> updateBuilder,
        TModel value,
        params string[]? excludeProperties)
    {
        var excludedPropertiesSet = new HashSet<string>(excludeProperties ?? Enumerable.Empty<string>());
        var updateDefinitions = new List<UpdateDefinition<TDocument>>();

        foreach (var property in value!.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!excludedPropertiesSet.Contains(property.Name))
            {
                updateDefinitions.Add(updateBuilder.Set(property.Name, property.GetValue(value)));
            }
        }

        return updateBuilder.Combine(updateDefinitions);
    }
}