// -------------------------------------------------------------------------------------
//  <copyright file="MongoConventions.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Infrastructure.Repositories;

using Domain.Primitives;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

public static class MongoConventions
{
    private static readonly Lock _lock = new();
    private static volatile bool _isMongoConventionsInitialized;

    public static void Initialize()
    {
        lock (_lock)
        {
            if (_isMongoConventionsInitialized) return;

            InitializeConventions();

            _isMongoConventionsInitialized = true;
        }
    }

    private static void InitializeConventions()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        ConventionRegistry.Register(
            "Camel Case",
            new ConventionPack { new CamelCaseElementNameConvention() },
            _ => true);

        ConventionRegistry.Register(
            "Ignore Extra Elements",
            new ConventionPack { new IgnoreExtraElementsConvention(true) },
            _ => true);

        ConventionRegistry.Register(
            "Ignore Nulls",
            new ConventionPack { new IgnoreIfNullConvention(true) },
            _ => true);

        ConventionRegistry.Register(
            "EnumStringConvention",
            new ConventionPack { new EnumRepresentationConvention(BsonType.String) },
            _ => true);

        BsonClassMap.RegisterClassMap<Entity<Guid>>(
            cm =>
            {
                cm.AutoMap();
                cm.UnmapProperty("Id");
                cm.MapMember(x => x.Id)
                    .SetOrder(0)
                    .SetElementName("id"); // This to prevent the field from being serialised as "_id"
            });
    }
}