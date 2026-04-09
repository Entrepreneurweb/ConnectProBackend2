using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.SharedKernel
{
    public abstract class Entity<TModel> : IAuditableEntity
    {
        public Id<TModel> Id { get; init; }
        public DateTimeOffset CreatedAtUtc { get; }
        public DateTimeOffset LastModifiedAtUtc { get; }

        protected Entity(Id<TModel> id)
        {
            Id = id;
            CreatedAtUtc = DateTimeOffset.UtcNow;
            LastModifiedAtUtc = DateTimeOffset.UtcNow;
        }

        protected Entity() : this(Id<TModel>.New()) { }
        public override bool Equals(object? obj)
        {
            if (obj is Entity<TModel> entity)
            {
                return entity.Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
