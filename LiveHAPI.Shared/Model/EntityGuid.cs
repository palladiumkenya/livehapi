using System;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Shared.Model
{
    public abstract class EntityGuid:Entity<Guid>
    {
        protected EntityGuid()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}