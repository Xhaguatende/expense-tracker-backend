// -------------------------------------------------------------------------------------
//  <copyright file="Entity.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace ExpenseTracker.Domain.Primitives;

public abstract class Entity<T> where T : notnull
{
    protected Entity(T id)
    {
        if (id is null || id.Equals(default(T)))
        {
            throw new ArgumentNullException(nameof(id));
        }

        Id = id;
    }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public T Id { get; private set; }

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = string.Empty;

    public int Version { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Entity<T> entity && EqualityComparer<T>.Default.Equals(Id, entity.Id);
    }

    public override int GetHashCode()
    {
        return GetHashCode(this);
    }

    public int GetHashCode(Entity<T> obj)
    {
        return obj.Id.GetHashCode();
    }
}