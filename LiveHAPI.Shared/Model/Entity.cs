using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.Model
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        [Key]
        public virtual TId Id { get; set; }
        public bool Voided { get; set; }

        protected Entity()
        {
        }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity<TId>;
            if (entity != null)
            {
                return Equals(entity);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        
        public bool Equals(IEntity<TId> other)
        {
            if (other == null)
            {
                return false;
            }
            return Id.Equals(other.Id);
        }
    }
}